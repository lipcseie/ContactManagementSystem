using Xunit;
using Moq;
using ContactManagementSystem.Entities.Models;
using ContactManagementSystem.BusinessLogicLayer.Services;
using ContactManagementSystem.Controllers;
using Microsoft.AspNetCore.Mvc;

public class HomeControllerTests
{
    private List<Contact> GetTestContacts()
    {
        var contacts = new List<Contact>();
        contacts.Add(new Contact() { Id = 1, Name = "Frodo Baggins", Country = "The Shire", Zip = 11111, City = "Hobbiton", Address = "Bag End", PhoneNumber = "1234567890" });
        contacts.Add(new Contact() { Id = 2, Name = "Samwise Gamgee", Country = "The Shire", Zip = 22222, City = "Hobbiton", Address = "Bagshot Row", PhoneNumber = "0987654321" });
        return contacts;
    }

    private readonly Mock<IContactService> mockService;
    private readonly HomeController controller;

    public HomeControllerTests()
    {
        mockService = new Mock<IContactService>();
        controller = new HomeController(mockService.Object);
    }

    [Fact]
    public async Task Index_ReturnsAViewResult_WithAListOfContacts()
    {
        // Arrange
        mockService.Setup(service => service.GetAllContactsAsync()).ReturnsAsync(GetTestContacts());

        // Act
       var result = await controller.Index();

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsAssignableFrom<List<Contact>>(viewResult.ViewData.Model);
        Assert.Equal(2, model.Count);
    }

    [Fact]
    public async Task Index_ReturnsViewResult_WithEmptyListOfContacts()
    {
        // Arrange
        mockService.Setup(service => service.GetAllContactsAsync()).ReturnsAsync(new List<Contact>());

        // Act
        var result = await controller.Index();

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsAssignableFrom<List<Contact>>(viewResult.Model);
        Assert.Empty(model);
    }

    [Fact]
    public async Task Index_ReturnsViewResult_WithCorrectModel()
    {
        // Arrange
        mockService.Setup(service => service.GetAllContactsAsync()).ReturnsAsync(GetTestContacts());

        // Act
        var result = await controller.Index();

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsAssignableFrom<List<Contact>>(viewResult.Model);
        Assert.Equal(2, model.Count);
    }

    [Fact]
    public async Task Create_InvalidModelState_ReturnsViewWithError()
    {
        // Arrange
        controller.ModelState.AddModelError("Name", "The Name field is required.");

        // Act
        var result = await controller.Create(new Contact());

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.False(viewResult.ViewData.ModelState.IsValid);
        Assert.Equal(1, viewResult.ViewData.ModelState.ErrorCount); 

    }

}
