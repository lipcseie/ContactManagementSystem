using ContactManagementSystem.Models;
using ContactManagementSystem.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ContactManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly IContactService _contactService;

        public HomeController(IContactService contactService)
        {
            _contactService = contactService;
        }

        public async Task<IActionResult> Index()
        {
            var contacts = await _contactService.GetAllContactsAsync();
            return View(contacts);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Contact contact)
        {
            if (ModelState.IsValid)
            {
                await _contactService.AddContactAsync(contact);
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        public async Task<IActionResult> Edit(int id)
        {
            var contact = await _contactService.GetContactByIdAsync(id);
            return View(contact);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Contact contact)
        {
            if (id != contact.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _contactService.UpdateContactAsync(contact);
                return RedirectToAction(nameof(Index));
            }
            return View(contact);
        }


        public async Task<IActionResult> Delete(int id)
        {
            var contact = await _contactService.GetContactByIdAsync(id);
            return View(contact);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfimed(int id)
        {
            await _contactService.DeleteContactAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
