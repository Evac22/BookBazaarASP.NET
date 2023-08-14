using Data.Entities;
using Data.Interfaces;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;
using UnitTests.Helpers;

namespace UnitTests.Repositories;

public class OrderedBookRepositoryTests
{
    private readonly Mock<IDataDbContext> _dbContextMock;

    private readonly OrderedBookRepository _repository;

    public OrderedBookRepositoryTests()
    {
        _dbContextMock = new Mock<IDataDbContext>();

        _repository = new OrderedBookRepository(_dbContextMock.Object);
    }

    [Fact]
    public void GetOrderedBook_ReturnsAllOrderedBook()
    {
        // Arrange
        var orderedBooks = new List<OrderedBook>
            {
                new OrderedBook { Id = Guid.NewGuid(), Book = new Book { Id = Guid.NewGuid(), Name = "Book 1" } },
                new OrderedBook { Id = Guid.NewGuid(), Book = new Book { Id = Guid.NewGuid(), Name = "Book 2" } },
                new OrderedBook { Id = Guid.NewGuid(), Book = new Book { Id = Guid.NewGuid(), Name = "Book 3" } }
            }.AsQueryable();

        var orderedBookDbSetMock = orderedBooks.AsQueryable().BuildMockDbSet();

        _dbContextMock.Setup(m => m.OrderedBooks).Returns(orderedBookDbSetMock);

        // Act
        var result = _repository.GetOrderedBook;

        // Assert
        Assert.Equal(3, result.Count());
    }

    [Fact]
    public void Get_ReturnsOrderedBookById()
    {
        // Arrange
        var orderedBook = new OrderedBook { Id = Guid.NewGuid(), Book = new Book { Id = Guid.NewGuid(), Name = "Book 1" } };

        var orderedBookDbSetMock = new List<OrderedBook> { orderedBook }.AsQueryable().BuildMockDbSet();

        _dbContextMock.Setup(m => m.OrderedBooks).Returns(orderedBookDbSetMock);

        // Act
        var result = _repository.Get(orderedBook.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(orderedBook.Id, result.Id);
    }

    [Fact]
    public void Add_AddsOrderedBookToDbContextAndSavesChanges()
    {   
        // Arrange
        var orderedBook = new OrderedBook { Id = Guid.NewGuid(), Book = new Book { Id = Guid.NewGuid(), Name = "Book 1" } };

        var orderedBookDbSetMock = new Mock<DbSet<OrderedBook>>();

        _dbContextMock.Setup(m => m.OrderedBooks).Returns(orderedBookDbSetMock.Object);

        // Act
        _repository.Add(orderedBook);

        // Assert
        orderedBookDbSetMock.Verify(m => m.Add(orderedBook), Times.Once);
        _dbContextMock.Verify(m => m.SaveChanges(), Times.Once);
    }
}