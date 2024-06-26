using ContactManagementSystem.BusinessLogicLayer.Services;
using ContactManagementSystem.DataAccessLayer.Repository;
using ContactManagementSystem.Entities.Models;
using Moq;

namespace ContactManagementSystem.Tests
{
    public class ContactRepositoryTests
    {
        [Fact]
        [Trait("ategory", "Repository")]
        public async Task GetAllContacts_WhenNoContactsExist_ReturnsEmptyList()
        {
            // Arrange
            var mockRepo = new Mock<IContactRepository>();
            mockRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(new List<Contact>());
            var contactService = new ContactService(mockRepo.Object);

            // Act
            var result = await contactService.GetAllContactsAsync();

            // Assert
            Assert.Empty(result);
        }
    }
}
