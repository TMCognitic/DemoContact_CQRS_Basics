using DemoContact.Infrastructure;
using DemoContact.Models.Commands.Contacts;
using DemoContact.Models.Entities;
using DemoContact.Models.Queries.Contacts;
using DemoContact.Models.ViewModels.Contacts;
using DemoContact.Tools.CQRS;
using DemoContact.Tools.CQRS.Commands;
using Microsoft.AspNetCore.Mvc;

namespace DemoContact.Controllers
{
    [AuthRequired]
    public class ContactController : BaseController
    {
        private readonly SessionManager _sessionManager;

        public ContactController(SessionManager sessionManager)
        {
            _sessionManager = sessionManager;
        }

        // GET: ContactController
        public IActionResult Index()
        {
            IEnumerable<Contact> contacts = Dispatcher.Dispatch(new GetContactListQuery(_sessionManager.UserId!.Value));
            return View(contacts.Select(c => c.ToDisplayContactLight()));
        }

        // GET: ContactController/Details/5
        public IActionResult Details(int id)
        {
            Contact? contact = Dispatcher.Dispatch(new GetContactQuery(_sessionManager.UserId!.Value, id));
            if (contact is null)
                return RedirectToAction("Index");

            return View(contact.ToDisplayContactFull());
        }

        // GET: ContactController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ContactController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateContactForm form)
        {
            if(!ModelState.IsValid)
                return View(form);

            Result result = Dispatcher.Dispatch(new AddContactCommand(form.LastName, form.FirstName, form.BirthDay, form.Email, form.Phone, _sessionManager.UserId!.Value));
            return FromResult(result, () => RedirectToAction("Index"));
        }

        // GET: ContactController/Edit/5
        public IActionResult Edit(int id)
        {
            Contact? contact = Dispatcher.Dispatch(new GetContactQuery(_sessionManager.UserId!.Value, id));
            if (contact is null)
                return RedirectToAction("Index");

            return View(contact.ToEditContactForm());
        }

        // POST: ContactController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, EditContactForm form)
        {
            if (!ModelState.IsValid)
                return View(form);

            Result result = Dispatcher.Dispatch(new EditContactCommand(id, form.LastName, form.FirstName, form.BirthDay, form.Email, form.Phone, _sessionManager.UserId!.Value));
            return FromResult(result, () => RedirectToAction("Index"));
        }

        // GET: ContactController/Delete/5
        public IActionResult Delete(int id)
        {
            Contact? contact = Dispatcher.Dispatch(new GetContactQuery(_sessionManager.UserId!.Value, id));
            if (contact is null)
                return RedirectToAction("Index");

            return View(contact.ToDisplayContactFull());
        }

        // POST: ContactController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id, IFormCollection collection)
        {
            Result result = Dispatcher.Dispatch(new DeleteContactCommand(id, _sessionManager.UserId!.Value));
            return FromResult(result, () => RedirectToAction("Index"));
        }
    }
}
