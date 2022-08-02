using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoContact.Models.Entities
{
    public class Contact
    {
        public int Id { get; set; }
        public string? LastName { get; set; }
        public string? FirstName { get; set; }
        public DateTime BirthDay { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public int UserId { get; set; }
    }
}
