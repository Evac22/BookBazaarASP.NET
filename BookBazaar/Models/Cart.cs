
using Data.Entities;

namespace BookBazaar.Models;

public class Cart
{
    private List<CartLine> lineCollection = new();

    public IEnumerable<CartLine> Lines => lineCollection;

    public void AddItem(Book book, int quantity)
    {
        CartLine line = lineCollection.FirstOrDefault(p => p.Book.Id == book.Id);

        if (line == null)
        {
            lineCollection.Add(new CartLine { Book = book, Quantity = quantity });
        }
        else
        {
            line.Quantity += quantity;
        }
    }

    public void RemoveLine(Book book)
    {
        lineCollection.RemoveAll(l => l.Book.Id == book.Id);
    }

    public decimal ComputeTotalValue()
    {
        return lineCollection.Sum(l => l.Book.Price * l.Quantity);
    }

    public void Clear()
    {
        lineCollection.Clear();
    }
}
