using ContactManagementSystem.BusinessLogicLayer.Services;
using ContactManagementSystem.DataAccessLayer.Repository;
using ContactManagementSystem.Entities.Models;
using Moq;

namespace ContactManagementSystem.Tests
{
    public class ContactRepositoryTests
    {
        private readonly Mock<IContactRepository> mockRepo;
        private readonly ContactService contactService;

        public ContactRepositoryTests()
        {
            mockRepo = new Mock<IContactRepository>();
            contactService = new ContactService(mockRepo.Object);
        }

        private Contact mockContact = new Contact { Id = 1, Name = "Bilbo Baggins" };


        [Fact]
        [Trait("Category", "ContactRepository_GetAllContacts")]
        public async Task GetAllContacts_WhenNoContactsExist_ReturnsEmptyList()
        {
            // Arrange
            mockRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(new List<Contact>());
            

            // Act
            var result = await contactService.GetAllContactsAsync();

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        [Trait("Category", "ContactRepository_GetAllContacts")]
        public async Task GetAllContacts_WhenContactsExist_ReturnsAllContacts()
        {
            // Arrange
            var mockContacts = new List<Contact>
            {
                new Contact { Id = 1, Name = "Frodo Baggins" },
                new Contact { Id = 2, Name = "Samwise Gamgee" }
            };
            mockRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(mockContacts);

            // Act
            var result = await contactService.GetAllContactsAsync();

            // Assert
            Assert.Equal(2, result.Count());
            Assert.Contains(result, c => c.Name == "Frodo Baggins");
            Assert.Contains(result, c => c.Name == "Samwise Gamgee");
        }

        [Fact]
        [Trait("Category", "ContactRepository_GetByIdAsync")]
        public async Task GetByIdAsync_WhenContactExists_ReturnsContact()
        {
            // Arrange
            mockRepo.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(mockContact);

            // Act
            var result = await contactService.GetContactByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("Bilbo Baggins", result.Name);

        }

        [Fact]
        [Trait("Category", "ContactRepository_GetByIdAsync")]
        public async Task GetByIdAsync_WhenContactDoesNotExist_ReturnsNull()
        {
            // Arrange
            mockRepo.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((Contact)null);

            // Act
            var result = await contactService.GetContactByIdAsync(1);

            // Assert
            Assert.Null(result);

        }
    }
}
