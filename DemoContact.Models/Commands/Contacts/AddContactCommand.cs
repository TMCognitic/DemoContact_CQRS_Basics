using DemoContact.Tools.Connections;
using DemoContact.Tools.CQRS.Commands;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoContact.Models.Commands.Contacts
{
    public class AddContactCommand : ICommand
    {
        public string? LastName { get; init; }
        public string? FirstName { get; init; }
        public DateTime? BirthDay { get;  init; }
        public string? Email { get;  init; }
        public string? Phone { get ; init; }
        public int UserId { get; init; }

        public AddContactCommand(string? lastName, string? firstName, DateTime? birthDay, string? email, string? phone, int userId)
        {
            LastName = lastName;
            FirstName = firstName;
            BirthDay = birthDay;
            Email = email;
            Phone = phone;
            UserId = userId;
        }


        public class AddContactCommandHandler : ICommandHandler<AddContactCommand>
        {
            private readonly DbConnection _dbConnection;

            public AddContactCommandHandler(DbConnection dbConnection)
            {
                _dbConnection = dbConnection;
            }

            public Result Handle(AddContactCommand command)
            {
                try
                {
                    using (_dbConnection)
                    {
                        int rows = _dbConnection.ExecuteNonQuery("Insert Into Contact (LastName, FirstName, BirthDay, Email, Phone, UserId) VALUES (@LastName, @FirstName, @BirthDay, @Email, @Phone, @UserId);",
                                                                 parameters: command);

                        if (rows != 1)
                            return Result.Failure("Nombre de lignes non valide lors de l'insertion du contact!");

                        return Result.Success();
                    }                
                }
                catch (Exception ex)
                {
                    return Result.Failure(ex.Message);
                }
            }
        }
    }
}
