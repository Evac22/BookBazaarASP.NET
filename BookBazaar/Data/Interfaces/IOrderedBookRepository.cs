using Data.Entities;

namespace Data.Interfaces;

public interface IOrderedBookRepository
{
    IEnumerable<OrderedBook> GetOrderedBook { get; }
    OrderedBook Get(Guid orderedBookId);
    void Add(OrderedBook orderedBook);
}
