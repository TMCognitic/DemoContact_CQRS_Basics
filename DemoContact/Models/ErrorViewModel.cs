using DemoContact.Tools.CQRS.Commands;

namespace DemoContact.Models
{
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        public Result? Result { get; set; }

        public ErrorViewModel()
        {

        }

        public ErrorViewModel(Result result)
        {
            Result = result;
        }
    }
}