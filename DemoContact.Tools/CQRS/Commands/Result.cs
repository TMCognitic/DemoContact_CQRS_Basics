using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoContact.Tools.CQRS.Commands
{
    public class Result
    {
        public bool IsSuccess { get; init; }
        public bool IsFailure => !IsSuccess;
        public string? Error { get; init; }

        private Result(bool isSuccess, string? error)
        {
            IsSuccess = isSuccess;
            Error = error;
        }

        public static Result Success()
        {
            return new Result(true, null);
        }

        public static Result Failure(string? error)
        {
            if(string.IsNullOrEmpty(error) || string.IsNullOrWhiteSpace(error))
                throw new ArgumentException(nameof(error), "Veuillez spécifier le message d'erreur");

            return new Result(false, error);
        }
    }
}
