using ContactManagementSystem.Entities.Models;
using ContactManagementSystem.BusinessLogicLayer.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
                try
                {
                    await _contactService.AddContactAsync(contact);
                    TempData["success"] = "CONTACT CREATED SUCCESFULLY";
                    return RedirectToAction(nameof(Index));

                }
                catch(DbUpdateException)
                {
                    ModelState.AddModelError("", "Unable to save changes. " + "A contact with the same name or phone number already exists.");

                }
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
                try
                {
                    await _contactService.UpdateContactAsync(contact);
                    TempData["success"] = "CONTACT UPDATED SUCCESFULLY";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "Unable to save changes. " + "A contact with the same name or phone number already exists.");

                }
            }
            return View(contact);
        }


        public async Task<IActionResult> Delete(int id)
        {
            var contact = await _contactService.GetContactByIdAsync(id);
            return View(contact);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfimed(int id, Contact contact)
        {
            try
            {
                await _contactService.DeleteContactAsync(id);
                TempData["success"] = "CONTACT DELETED SUCCESFULLY";
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Unable to delete contact. " + "The contact might have already been deleted or does not exist.");
            }
            return View(contact);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
