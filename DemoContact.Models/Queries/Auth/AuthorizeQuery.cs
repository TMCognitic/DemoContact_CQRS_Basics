using DemoContact.Tools.Connections;
using DemoContact.Tools.CQRS.Queries;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoContact.Models.Queries.Auth
{
    public class AuthorizeQuery : IQuery<int>
    {
        public string Email { get; init; }
        public string Passwd { get; init; }

        public AuthorizeQuery(string email, string passwd)
        {
            Email = email;
            Passwd = passwd;
        }

        public class AuthorizeQueryHandler : IQueryHandler<AuthorizeQuery, int>
        {
            private readonly DbConnection _dbConnection;

            public AuthorizeQueryHandler(DbConnection dbConnection)
            {
                _dbConnection = dbConnection;
            }

            public int Handle(AuthorizeQuery query)
            {
                using (_dbConnection)
                {
                    object? result = _dbConnection.ExecuteScalar("TSP_Authorize", true, query);

                    if (result is null)
                        return -1;

                    return (int)result;
                }
            }
        }
    }
}
