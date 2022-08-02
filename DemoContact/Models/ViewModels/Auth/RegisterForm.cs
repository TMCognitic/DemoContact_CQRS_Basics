using System.ComponentModel.DataAnnotations;

namespace DemoContact.Models.ViewModels.Auth
{
    public class RegisterForm
    {
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string? Passwd { get; set; }
        [Compare(nameof(Passwd))]
        [DataType(DataType.Password)]
        public string? Confirm { get; set; }
    }
}
