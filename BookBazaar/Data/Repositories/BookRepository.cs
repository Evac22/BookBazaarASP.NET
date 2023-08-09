using Data.Entities;
using Data.Interfaces;

namespace Data.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly IDataDbContext _dbContext;

        public BookRepository(IDataDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Book> GetBook => _dbContext.Books;

        public Book Get(Guid bookId) => _dbContext.Books.Find(bookId);

        public void Add(Book book)
        {
            if (book.Id == Guid.Empty)
            {
                book.Id = Guid.NewGuid();
            }
            _dbContext.Books.Add(book);

            _dbContext.SaveChanges();
        }

        public Book Update(Book book)
        {
            var existingBook = _dbContext.Books.Find(book.Id);
            if (existingBook != null)
            {
                existingBook.Name = book.Name;
                existingBook.Description = book.Description;
                existingBook.Genre = existingBook.Genre;
                existingBook.Price = book.Price;
                _dbContext.SaveChanges();
            }
            return existingBook;
        }

        public void Delete(Guid bookId)
        {
            var book = _dbContext.Books.Find(bookId);
            if (book != null)
            {
                _dbContext.Books.Remove(book);
                _dbContext.SaveChanges();
            }
        }
    }
}



