using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace DemoContact.Models.ViewModels.Contacts
{
    public class EditContactForm
    {
        [HiddenInput]
        public int Id { get; set; }
        [Required]
        public string? LastName { get; set; }
        [Required]
        public string? FirstName { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime? BirthDay { get; set; }
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        public string? Phone { get; set; }
    }
}