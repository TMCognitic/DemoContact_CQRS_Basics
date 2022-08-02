using DemoContact.Tools.Connections;
using DemoContact.Tools.CQRS.Commands;
using System.Data.Common;

namespace DemoContact.Models.Commands.Contacts
{
    public class DeleteContactCommand : ICommand
    {
        public int Id { get; init; }
        public int UserId { get; init; }

        public DeleteContactCommand(int id, int userId)
        {
            Id = id;
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
                        int rows = _dbConnection.ExecuteNonQuery("Delete From Contact Where UserId = @UserId AND Id = @Id And IsDeleted = 0;",
                                                                 parameters: command);

                        if (rows != 1)
                            return Result.Failure("Nombre de lignes non valide lors de la suppression du contact!");

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
