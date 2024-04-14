using ContactManagementSystem.Models;

namespace ContactManagementSystem.Services
{
    public interface IContactService
    {
        Task<IEnumerable<Contact>> GetAllContactsAsync();

        Task AddContactAsync(Contact contact);

        Task UpdateContactAsync(Contact contact);

        Task DeleteContactAsync(int id);
    }
}
