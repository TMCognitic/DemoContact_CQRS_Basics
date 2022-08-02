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
    public class EditContactCommand : ICommand
    {
        public int Id { get; init; }
        public string? LastName { get; init; }
        public string? FirstName { get; init; }
        public DateTime? BirthDay { get; init; }
        public string? Email { get; init; }
        public string? Phone { get; init; }
        public int UserId { get; init; }

        public EditContactCommand(int id, string? lastName, string? firstName, DateTime? birthDay, string? email, string? phone, int userId)
        {
            Id = id;
            LastName = lastName;
            FirstName = firstName;
            BirthDay = birthDay;
            Email = email;
            Phone = phone;
            UserId = userId;
        }


        public class EditContactCommandHandler : ICommandHandler<EditContactCommand>
        {
            private readonly DbConnection _dbConnection;

            public EditContactCommandHandler(DbConnection dbConnection)
            {
                _dbConnection = dbConnection;
            }

            public Result Handle(EditContactCommand command)
            {
                try
                {
                    using (_dbConnection)
                    {
                        int rows = _dbConnection.ExecuteNonQuery("Update Contact Set LastName = @LastName, FirstName = @FirstName, BirthDay = @BirthDay, Email = @Email, Phone = @Phone Where UserId = @UserId AND Id = @Id And IsDeleted = 0;",
                                                                 parameters: command);

                        if (rows != 1)
                            return Result.Failure("Nombre de lignes non valide lors de la mise à jour du contact!");

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
