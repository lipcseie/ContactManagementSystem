using ContactManagementSystem.Entities.Models;

namespace ContactManagementSystem.BusinessLogicLayer.Services
{
    public interface IContactService
    {
        Task<IEnumerable<Contact>> GetAllContactsAsync();

        Task<Contact> GetContactByIdAsync(int  id); 

        Task AddContactAsync(Contact contact);

        Task UpdateContactAsync(Contact contact);

        Task DeleteContactAsync(int id);
    }
}
