using Xunit;
using Moq;
using ContactManagementSystem.Entities.Models;
using ContactManagementSystem.BusinessLogicLayer.Services;
using ContactManagementSystem.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

public class HomeControllerTests
{
    private List<Contact> GetTestContacts()
    {
        var contacts = new List<Contact>();
        contacts.Add(new Contact() { Id = 1, Name = "Frodo Baggins", Country = "The Shire", Zip = 11111, City = "Hobbiton", Address = "Bag End", PhoneNumber = "1234567890" });
        contacts.Add(new Contact() { Id = 2, Name = "Samwise Gamgee", Country = "The Shire", Zip = 22222, City = "Hobbiton", Address = "Bagshot Row", PhoneNumber = "0987654321" });
        return contacts;
    }

    [Fact]
    public async Task Index_ReturnsAViewResult_WithAListOfContacts()
    {
        // Arrange
        var mockService = new Mock<IContactService>();
        mockService.Setup(service => service.GetAllContactsAsync()).ReturnsAsync(GetTestContacts());
        var controller = new HomeController(mockService.Object);

        // Act
       var result = await controller.Index();

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsAssignableFrom<List<Contact>>(viewResult.ViewData.Model);
        Assert.Equal(2, model.Count);
    }

}
