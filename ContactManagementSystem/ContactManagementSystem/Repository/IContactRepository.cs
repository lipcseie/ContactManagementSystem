using ContactManagementSystem.Models;

namespace ContactManagementSystem.Repository
{
    public interface IContactRepository
    {
        Task<IEnumerable<Contact>> GetAllAsync();

        Task AddContactAsync(Contact contact);

        Task UpdateContactAsync(Contact contact);

        Task DeleteContactAsync(int id);
    }
}
