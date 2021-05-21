using GestContact.Web.Models.Entities;
using GestContact.Web.Models.Forms;
using GestContact.Web.Models.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestContact.Web.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactRepository _repository;

        public ContactController(IContactRepository repository)
        {
            _repository = repository;
        }

        // GET: ContactController
        public ActionResult Index()
        {
            return View(_repository.Get());
        }

        // GET: ContactController/Details/5
        public ActionResult Details(int id)
        {
            Contact contact = _repository.Get(id);

            if (contact is null)
                return RedirectToAction("Index");

            return View(contact);
        }

        // GET: ContactController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ContactController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AddContactForm form)
        {
            if(ModelState.IsValid)
            {
                Contact contact = new Contact() { LastName = form.LastName, FirstName = form.FirstName, Email = form.Email, Phone = form.Phone, Birthdate = form.Birthdate };
                _repository.Insert(contact);
                return RedirectToAction("Index");
            }

            return View(form);
        }

        // GET: ContactController/Edit/5
        public ActionResult Edit(int id)
        {
            Contact contact = _repository.Get(id);

            if (contact is null)
                return RedirectToAction("Index");

            return View(new UpdateContactForm() { Id = contact.Id, LastName = contact.LastName, FirstName = contact.FirstName, Email = contact.Email, Phone = contact.Phone, Birthdate = contact.Birthdate });
        }

        // POST: ContactController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, UpdateContactForm form)
        {
            if (ModelState.IsValid)
            {
                if(id == form.Id)
                {
                    Contact contact = new Contact() { Id = form.Id, LastName = form.LastName, FirstName = form.FirstName, Email = form.Email, Phone = form.Phone, Birthdate = form.Birthdate };
                    _repository.Update(id, contact);
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Edit", new { id = form.Id });
                }
            }

            return View(form);
        }

        // GET: ContactController/Delete/5
        public ActionResult Delete(int id)
        {
            Contact contact = _repository.Get(id);

            if (contact is null)
                return RedirectToAction("Index");

            return View(contact);
        }

        // POST: ContactController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            _repository.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
