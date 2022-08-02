using DemoContact.Models.Entities;
using DemoContact.Models.Mappers;
using DemoContact.Tools.Connections;
using DemoContact.Tools.CQRS.Queries;
using System.Data.Common;

namespace DemoContact.Models.Queries.Contacts
{
    public class GetContactListQuery : IQuery<IEnumerable<Contact>>
    {
        public int UserId { get; init; }

        public GetContactListQuery(int userId)
        {
            UserId = userId;
        }

        public class GetContactListQueryHandler : IQueryHandler<GetContactListQuery, IEnumerable<Contact>>
        {
            private readonly DbConnection _dbConnection;

            public GetContactListQueryHandler(DbConnection dbConnection)
            {
                _dbConnection = dbConnection;
            }

            public IEnumerable<Contact> Handle(GetContactListQuery query)
            {
                using (_dbConnection)
                {
                    return _dbConnection.ExecuteReader("Select Id, LastName, FirstName, BirthDay, Email, Phone, UserId FROM Contact WHERE UserId = @UserId And IsDeleted = 0",
                                                       (dr) => dr.ToContact(),
                                                       immediately: true,
                                                       parameters: query);
                }
            }
        }
    }
}
