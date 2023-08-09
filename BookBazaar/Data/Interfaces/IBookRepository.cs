using Data.Entities;

namespace Data.Interfaces;
public interface IBookRepository
{
    IEnumerable<Book> GetBook { get; }
    Book Get(Guid bookId);
    void Add(Book book);
    Book Update(Book book);
    void Delete(Guid bookId);
}

