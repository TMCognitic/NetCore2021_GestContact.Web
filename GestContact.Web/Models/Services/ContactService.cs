using GestContact.Web.Models.Entities;
using GestContact.Web.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace GestContact.Web.Models.Services
{
    public class ContactService : IContactRepository
    {
        private readonly HttpClient _httpClient;

        public ContactService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }        

        public IEnumerable<Contact> Get()
        {
            HttpResponseMessage httpResponseMessage = _httpClient.GetAsync("api/contact").Result;
            httpResponseMessage.EnsureSuccessStatusCode();
            string result = httpResponseMessage.Content.ReadAsStringAsync().Result;
            return JsonSerializer.Deserialize<Contact[]>(result, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public Contact Get(int id)
        {
            HttpResponseMessage httpResponseMessage = _httpClient.GetAsync("api/contact/" + id).Result;
            httpResponseMessage.EnsureSuccessStatusCode();

            if (httpResponseMessage.StatusCode == HttpStatusCode.NoContent)
                return null;

            string result = httpResponseMessage.Content.ReadAsStringAsync().Result;            
            return JsonSerializer.Deserialize<Contact>(result, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public IEnumerable<Contact> Get(string name)
        {
            HttpResponseMessage httpResponseMessage = _httpClient.GetAsync("api/contact/byname/" + name).Result;
            httpResponseMessage.EnsureSuccessStatusCode();
            string result = httpResponseMessage.Content.ReadAsStringAsync().Result;
            return JsonSerializer.Deserialize<Contact[]>(result, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public void Insert(Contact contact)
        {
            string json = JsonSerializer.Serialize(contact);
            HttpContent httpContent = new StringContent(json);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            HttpResponseMessage httpResponseMessage = _httpClient.PostAsync("api/contact/", httpContent).Result;
            httpResponseMessage.EnsureSuccessStatusCode();
        }

        public bool Update(int id, Contact contact)
        {
            string json = JsonSerializer.Serialize(contact);
            HttpContent httpContent = new StringContent(json);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            HttpResponseMessage httpResponseMessage = _httpClient.PutAsync("api/contact/" + id, httpContent).Result;
            return httpResponseMessage.IsSuccessStatusCode;
        }

        public bool Delete(int id)
        {
            HttpResponseMessage httpResponseMessage = _httpClient.DeleteAsync("api/contact/" + id).Result;
            return httpResponseMessage.IsSuccessStatusCode;
        }
    }
}
