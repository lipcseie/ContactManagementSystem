using ContactManagementSystem.Entities.Models;

namespace ContactManagementSystem.DataAccessLayer.Repository
{
    public interface IContactRepository
    {
        Task<IEnumerable<Contact>> GetAllAsync();

        Task<Contact> GetByIdAsync(int id);

        Task AddContactAsync(Contact contact);

        Task UpdateContactAsync(Contact contact);

        Task DeleteContactAsync(int id);
    }
}
