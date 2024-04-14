using ContactManagementSystem.Models;

namespace ContactManagementSystem.Repository
{
    public class ContactRepository : IContactRepository
    {
        private readonly ContactsContexts _contacts;
        public Task AddContact(Contact contact)
        {
            throw new NotImplementedException();
        }

        public Task DeleteContact(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Contact>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Contact> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateContact(Contact contact)
        {
            throw new NotImplementedException();
        }
    }
}
