using DemoContact.Models;
using DemoContact.Tools.CQRS;
using DemoContact.Tools.CQRS.Commands;
using Microsoft.AspNetCore.Mvc;

namespace DemoContact.Controllers
{
    public abstract class BaseController : Controller
    {
        private Dispatcher? _dispatcher;

        protected Dispatcher Dispatcher
        {
            get
            {
                return _dispatcher ??= HttpContext.RequestServices.GetService<Dispatcher>()!;
            }
        }

        protected IActionResult FromResult(Result result, Func<IActionResult> actionIfSuccess)
        {
            if(result.IsSuccess && actionIfSuccess is null)
                return View("Error", new ErrorViewModel(Result.Failure("Aucune action définie en cas de succès")));

            if(result.IsFailure)
                return View("Error", new ErrorViewModel(result));

            return actionIfSuccess();
        }
    }
}