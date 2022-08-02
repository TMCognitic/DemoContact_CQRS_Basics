using DemoContact.Tools.Connections;
using DemoContact.Tools.CQRS.Commands;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoContact.Models.Commands.Auth
{
    public class RegisterCommand : ICommand
    {
        public string Email { get; init; }
        public string Passwd { get; init; }
        public RegisterCommand(string email, string passwd)
        {
            Email = email;
            Passwd = passwd;
        }

        public class RegisterCommandHandler : ICommandHandler<RegisterCommand>
        {
            private readonly DbConnection _dbConnection;

            public RegisterCommandHandler(DbConnection dbConnection)
            {
                _dbConnection = dbConnection;
            }

            public Result Handle(RegisterCommand command)
            {
                try
                {
                    int rows = _dbConnection.ExecuteNonQuery("TSP_Register", true, command);

                    if (rows == 0)
                        return Result.Failure("Aucune donnée insérée!");

                    return Result.Success();
                }
                catch (Exception ex)
                {
                    return Result.Failure(ex.Message);
                }
            }
        }
    }
}
