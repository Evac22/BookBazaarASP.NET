using Data.Entities;
using Data.Interfaces;
using Data.Repositories;
using Moq;
using UnitTests.Helpers;

namespace UnitTests.Repositories
{
    public class BookRepositoryTests
    {
        private readonly Mock<IDataDbContext> _dbContextMock;
        private readonly BookRepository _bookRepository;

        public BookRepositoryTests()
        {
            _dbContextMock = new Mock<IDataDbContext>();
            _bookRepository = new BookRepository(_dbContextMock.Object);
        }

        [Fact]
        public void GetBook_Returns_All_Books()
        {
            // Arrange
            var books = new List<Book>()
        {
            new Book { Id = Guid.NewGuid(), Name = "Book 1", Category = "Category 1", Price = 10.00M },
            new Book { Id = Guid.NewGuid(), Name = "Book 2", Category = "Category 2", Price = 15.50M },
            new Book { Id = Guid.NewGuid(), Name = "Book 3", Category = "Category 3", Price = 20.00M }
        }.AsQueryable();


            var bookDbSetMock = books.AsQueryable().BuildMockDbSet();

            _dbContextMock.Setup(m => m.Books).Returns(bookDbSetMock);

            // Act
            var result = _bookRepository.GetBook;

            // Assert
            Assert.Equal(3, result.Count());
        }

        [Fact]
        public void Get_Returns_Book_By_Id()
        {
            // Arrange
            var bookId = Guid.NewGuid();
            var book = new Book { Id = bookId, Name = "Book 1", Category = "Category 1", Price = 10.00M };
            _dbContextMock.Setup(db => db.Books.Find(bookId)).Returns(book);

            // Act
            var result = _bookRepository.Get(bookId);

            // Assert
            Assert.Equal(book, result);
        }

        [Fact]
        public void Add_Adds_New_Book()
        {
            // Arrange
            var book = new Book { Name = "Book 1", Category = "Category 1", Price = 10.00M };
            _dbContextMock.Setup(db => db.Books.Add(book));

            // Act
            _bookRepository.Add(book);

            // Assert
            _dbContextMock.Verify(db => db.SaveChanges(), Times.Once);
        }

        [Fact]
        public void Add_Assigns_New_Id_If_Id_Is_Empty()
        {
            // Arrange
            var book = new Book { Name = "Book 1", Category = "Category 1", Price = 10.00M };
            _dbContextMock.Setup(db => db.Books.Add(book));

            // Act
            _bookRepository.Add(book);

            // Assert
            Assert.NotEqual(Guid.Empty, book.Id);
            _dbContextMock.Verify(db => db.Books.Add(book), Times.Once);
            _dbContextMock.Verify(db => db.SaveChanges(), Times.Once);
        }

        [Fact]
        public void Add_Does_Not_Assign_New_Id_If_Id_Is_Not_Empty()
        {
            // Arrange
            var bookId = Guid.NewGuid();
            var book = new Book { Id = bookId, Name = "Book 1", Category = "Category 1", Price = 10.00M };
            _dbContextMock.Setup(db => db.Books.Add(book));

            // Act
            _bookRepository.Add(book);

            // Assert
            Assert.Equal(bookId, book.Id);
            _dbContextMock.Verify(db => db.Books.Add(book), Times.Once);
            _dbContextMock.Verify(db => db.SaveChanges(), Times.Once);
        }

        [Fact]
        public void Update_NonExistingBook_ReturnsNull()
        {
            // Arrange
            var book = new Book
            {
                Id = Guid.NewGuid(),
                Name = "New Book",
                Description = "New Book Description",
                Category = "New Category",
                Price = 5.99m
            };

            _dbContextMock.Setup(m => m.Books.Find(book.Id)).Returns((Book)null);

            // Act
            var result = _bookRepository.Update(book);

            // Assert
            Assert.Null(result);
            _dbContextMock.Verify(m => m.SaveChanges(), Times.Never);
        }

        // Test deleting a food
        [Fact]
        public void Delete_ExistingBook_RemovesFood()
        {
            // Arrange
            var bookId = Guid.NewGuid();
            var book = new Book
            {
                Id = bookId,
                Name = "Existing Book",
                Description = "Existing Book Description",
                Category = "Existing Category",
                Price = 10.99m
            };

            _dbContextMock.Setup(m => m.Books.Find(bookId)).Returns(book);

            // Act
            _bookRepository.Delete(bookId);

            // Assert
            _dbContextMock.Verify(m => m.Books.Remove(book), Times.Once);
            _dbContextMock.Verify(m => m.SaveChanges(), Times.Once);
        }

        // Test deleting a non-existing food
        [Fact]
        public void Delete_NonExistingBook_DoesNothing()
        {
            // Arrange
            var bookId = Guid.NewGuid();

            _dbContextMock.Setup(m => m.Books.Find(bookId)).Returns((Book)null);

            // Act
            _bookRepository.Delete(bookId);

            // Assert
            _dbContextMock.Verify(m => m.Books.Remove(It.IsAny<Book>()), Times.Never);
            _dbContextMock.Verify(m => m.SaveChanges(), Times.Never);
        }

    }
}
