using BookBazaar.Controllers;
using Data.Entities;
using Data.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace UnitTests.Controllers;

public class AdminControllerTests
{
    [Fact]
    public void BookList_ReturnsViewResult_WithListOfBook()
    {
        // Arrange
        var mockBookRepository = new Mock<IBookRepository>();
        var expectedBooks = new List<Book> { new Book { Id = Guid.NewGuid(), Name = "Book 1", Price = 10 } };
        mockBookRepository.Setup(repo => repo.GetBook).Returns(expectedBooks);
        var controller = new AdminController(mockBookRepository.Object, null, null);

        // Act
        var result = controller.BookList();

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsAssignableFrom<IEnumerable<Book>>(viewResult.ViewData.Model);
        Assert.Equal(expectedBooks, model);
    }

    [Fact]
    public void Add_WhenModelStateIsInvalid_ReturnsViewResultWithBook()
    {
        // Arrange
        var mockBookRepository = new Mock<IBookRepository>();
        var bookToAdd = new Book { Name = "Book 1", Price = -10 };
        var controller = new AdminController(mockBookRepository.Object, null, null);
        controller.ModelState.AddModelError("Price", "Price must be greater than zero");

        // Act
        var result = controller.Add(bookToAdd) as ViewResult;

        // Assert
        Assert.IsType<Book>(result.ViewData.Model);
        Assert.Equal(bookToAdd, result.ViewData.Model);
    }

    [Fact]
    public void Edit_ReturnsViewResult_WithBookModel()
    {
        // Arrange
        var book = new Book { Id = Guid.NewGuid(), Name = "Test Book", Description = "Test description", Price = 9.99m };
        var mockBookRepository = new Mock<IBookRepository>();
        mockBookRepository.Setup(repo => repo.GetBook).Returns(new List<Book> { book }.AsQueryable());
        var controller = new AdminController(mockBookRepository.Object, null, null);

        // Act
        var result = controller.Edit(book.Id);

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsAssignableFrom<Book>(viewResult.ViewData.Model);
        Assert.Equal(book, model);
    }

    [Fact]
    public void Edit_ReturnsViewResult_WithBookModel_WhenModelStateIsInvalid()
    {
        // Arrange
        var book = new Book { Id = Guid.NewGuid(), Name = "Test Book", Description = "Test description", Price = 9.99m };
        var mockBookRepository = new Mock<IBookRepository>();
        var controller = new AdminController(mockBookRepository.Object, null, null);
        controller.ModelState.AddModelError("Name", "Required");

        // Act
        var result = controller.Edit(book);

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsAssignableFrom<Book>(viewResult.ViewData.Model);
        Assert.Equal(book, model);
    }

    [Fact]
    public void Delete_RedirectsToBookList_WhenBookIsDeleted()
    {
        // Arrange
        var bookId = Guid.NewGuid();
        var mockBookRepository = new Mock<IBookRepository>();
        var controller = new AdminController(mockBookRepository.Object, null, null);

        // Act
        var result = controller.Delete(new Book { Id = bookId });

        // Assert
        mockBookRepository.Verify(repo => repo.Delete(bookId), Times.Once);
        var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("BookList", redirectToActionResult.ActionName);
    }
}