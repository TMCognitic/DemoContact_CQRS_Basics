using System.ComponentModel.DataAnnotations;

namespace DemoContact.Models.ViewModels.Auth
{
    public class LoginForm
    {
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string? Passwd { get; set; }
    }
}
