using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class OrderedBookRepository : IOrderedBookRepository
    {
        private readonly IDataDbContext _dbContext;

        public OrderedBookRepository(IDataDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<OrderedBook> GetOrderedBook => _dbContext.OrderedBooks.Include(ob => ob.Book).ToList();

        public OrderedBook Get(Guid orderedBookId)
        {
            return _dbContext.OrderedBooks.Include(ob => ob.Book).FirstOrDefault(o => o.Id == orderedBookId);
        }

        public void Add(OrderedBook orderedBook)
        {
            if (orderedBook.Id == Guid.Empty)
            {
                orderedBook.Id = Guid.NewGuid();
                _dbContext.OrderedBooks.Add(orderedBook);
            }
            _dbContext.OrderedBooks.Add(orderedBook);

            _dbContext.SaveChanges();
        }
    }
}
