using DemoContact.Models.Entities;
using DemoContact.Models.Mappers;
using DemoContact.Tools.Connections;
using DemoContact.Tools.CQRS.Queries;
using System.Data.Common;

namespace DemoContact.Models.Queries.Contacts
{
    public class GetContactQuery : IQuery<Contact?>
    {
        public int UserId { get; init; }
        public int Id { get; set; }

        public GetContactQuery(int userId, int id)
        {
            UserId = userId;
            Id = id;
        }

        public class GetContactListQueryHandler : IQueryHandler<GetContactQuery, Contact?>
        {
            private readonly DbConnection _dbConnection;

            public GetContactListQueryHandler(DbConnection dbConnection)
            {
                _dbConnection = dbConnection;
            }

            public Contact? Handle(GetContactQuery query)
            {
                using (_dbConnection)
                {
                    return _dbConnection.ExecuteReader("Select Id, LastName, FirstName, BirthDay, Email, Phone, UserId FROM Contact WHERE UserId = @UserId And Id = @Id And IsDeleted = 0;",
                                                       (dr) => dr.ToContact(),
                                                       immediately: true,
                                                       parameters: query).SingleOrDefault();
                }
            }
        }
    }
}
