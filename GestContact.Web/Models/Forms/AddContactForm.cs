using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GestContact.Web.Models.Forms
{
    public class AddContactForm
    {
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }
        [EmailAddress]
        [MaxLength(384)]
        public string Email { get; set; }
        [MaxLength(50)]
        public string Phone { get; set; }
        [DataType(DataType.Date)]
        public DateTime? Birthdate { get; set; }
    }
}
