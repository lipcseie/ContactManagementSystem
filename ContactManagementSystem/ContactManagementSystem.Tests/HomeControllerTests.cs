using Xunit;
using Moq;
using ContactManagementSystem.Entities.Models;
using ContactManagementSystem.BusinessLogicLayer.Services;
using ContactManagementSystem.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;

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

        // Setup tempdata
        var tempData = new Mock<ITempDataDictionary>();
        tempData.Setup(t => t["succsess"]).Returns("CONTACT CREATED SUCCESFULLY");
        controller = new HomeController(mockService.Object)
        {
            TempData = tempData.Object
        };

    }

    [Fact]
    [Trait("Category", "Index")]
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
    [Trait("Category", "Index")]
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
    [Trait("Category", "Index")]
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
    [Trait("Category", "Create")]
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

    [Fact]
    [Trait("Category","Create")]
    public async Task Create_ValidModel_RedirectsToIndex()
    {
        // Arrange
        var contact = new Contact();
        controller.ModelState.Clear();
        mockService.Setup(service => service.AddContactAsync(contact)).Returns(Task.CompletedTask);

        // Act
        var result = await controller.Create(contact);

        // Assert
        var redirectResult = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("Index", redirectResult.ActionName);
        Assert.Equal("CONTACT CREATED SUCCESFULLY", controller.TempData["succsess"]);
        mockService.Verify(service => service.AddContactAsync(contact), Times.Once);
    }

    [Fact]
    [Trait("Category", "Create")]
    public async Task Create_DbUpdateException_ReturnsViewWithErrorMessage()
    {
        // Arrange
        var contact = new Contact { Name = "Bilbo Baggins", PhoneNumber = "1112223333" };
        controller.ModelState.Clear();
        mockService.Setup(service => service.AddContactAsync(contact)).ThrowsAsync(new DbUpdateException());

        // Act
        var result = await controller.Create(contact);

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.True(viewResult.ViewData.ModelState.ErrorCount > 0);
        Assert.Equal("Unable to save changes. A contact with the same name or phone number already exists.", viewResult.ViewData.ModelState[string.Empty].Errors.First().ErrorMessage);
        mockService.Verify(service => service.AddContactAsync(contact), Times.Once);
    }

    [Fact]
    [Trait("Categry", "Edit")]
    public async Task Edit_ValidId_ReturnsViewResultWithContact()
    {
        // Arrange
        var contact = GetTestContacts().First();
        mockService.Setup(service => service.GetContactByIdAsync(contact.Id)).ReturnsAsync(contact);

        // Act
        var result = await controller.Edit(contact.Id);

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsAssignableFrom<Contact>(viewResult.ViewData.Model);
        Assert.Equal(contact.Id, model.Id);
    }
}
