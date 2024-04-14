using ContactManagementSystem.Models;
using ContactManagementSystem.Context;
using Microsoft.EntityFrameworkCore;

namespace ContactManagementSystem.Repository
{
    public class ContactRepository : IContactRepository
    {
        private readonly ContactsContext _context;

        public ContactRepository(ContactsContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Contact>> GetAllAsync()
        {
            return await _context.Contacts.ToListAsync();
        }


        public async Task AddContactAsync(Contact contact)
        {
            _context.Contacts.Add(contact);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateContactAsync(Contact contact)
        {
            _context.Entry(contact).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        public async Task DeleteContactAsync(int id)
        {
            var contact = await _context.Contacts.FindAsync(id);
            _context.Contacts.Remove(contact);
            await _context.SaveChangesAsync();
        }

    }
}
