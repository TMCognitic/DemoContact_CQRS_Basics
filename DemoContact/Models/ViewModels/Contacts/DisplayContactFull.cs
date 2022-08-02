using System.ComponentModel.DataAnnotations;

namespace DemoContact.Models.ViewModels.Contacts
{
    public class DisplayContactFull
    {
        public int Id { get; set; }
        public string? LastName { get; set; }
        public string? FirstName { get; set; }
        [DataType(DataType.Date)]
        public DateTime? BirthDay { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
    }
}