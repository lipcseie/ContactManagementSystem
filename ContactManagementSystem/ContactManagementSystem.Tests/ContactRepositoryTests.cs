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

        [Fact]
        [Trait("Category", "Repository")]
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
        [Trait("Category", "Repository")]
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
    }
}
