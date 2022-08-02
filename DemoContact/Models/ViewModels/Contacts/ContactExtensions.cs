using DemoContact.Models.Entities;

namespace DemoContact.Models.ViewModels.Contacts
{
    public static class ContactExtensions
    {
        public static DisplayContactLight ToDisplayContactLight(this Contact entity)
        {
            return new DisplayContactLight() { Id = entity.Id, LastName = entity.LastName, FirstName = entity.FirstName };
        }

        public static DisplayContactFull ToDisplayContactFull(this Contact entity)
        {
            return new DisplayContactFull() { Id = entity.Id, LastName = entity.LastName, FirstName = entity.FirstName, BirthDay = entity.BirthDay, Email = entity.Email, Phone = entity.Phone };
        }

        public static EditContactForm ToEditContactForm(this Contact entity)
        {
            return new EditContactForm() { Id = entity.Id, LastName = entity.LastName, FirstName = entity.FirstName, BirthDay = entity.BirthDay, Email = entity.Email, Phone = entity.Phone };
        }
    }
}
