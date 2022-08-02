using DemoContact.Infrastructure;
using DemoContact.Models.Commands.Auth;
using DemoContact.Models.Queries.Auth;
using DemoContact.Models.ViewModels.Auth;
using DemoContact.Tools.CQRS.Commands;
using Microsoft.AspNetCore.Mvc;

namespace DemoContact.Controllers
{
    public class AuthController : BaseController
    {
        private readonly SessionManager _sessionManager;

        public AuthController(SessionManager sessionManager)
        {
            _sessionManager = sessionManager;
        }

        public IActionResult Index()
        {
            return RedirectToAction("Login");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginForm form)
        {
            if(!ModelState.IsValid)
                return View(form);

            int id = Dispatcher.Dispatch(new AuthorizeQuery(form.Email!, form.Passwd!));

            if(id == -1)
            {
                ModelState.AddModelError("", "Bad email or password!");
                return View(form);
            }

            _sessionManager.UserId = id;
            return RedirectToAction("Index", "Contact");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterForm form)
        {
            if (!ModelState.IsValid)
                return View(form);

            Result result = Dispatcher.Dispatch(new RegisterCommand(form.Email!, form.Passwd!));
            return FromResult(result, () => RedirectToAction("Login"));
        }

        public IActionResult Logout()
        {
            _sessionManager.Abandon();
            return RedirectToAction("Index", "Home");
        }
    }
}
