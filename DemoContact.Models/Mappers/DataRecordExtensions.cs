using DemoContact.Models.Entities;
using System.Data;

namespace DemoContact.Models.Mappers
{
    internal static class DataRecordExtensions
    {
        public static Contact ToContact(this IDataRecord dataRecord)
        {
            return new Contact()
            {
                Id = (int)dataRecord["Id"],
                LastName = (string)dataRecord["LastName"],
                FirstName = (string)dataRecord["FirstName"],
                BirthDay = (DateTime)dataRecord["BirthDay"],
                Email = (string)dataRecord["Email"],
                Phone = dataRecord["Phone"] as string,
                UserId = (int)dataRecord["UserId"]
            };
        }
    }
}
