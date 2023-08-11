
using Data.Entities;

namespace BookBazaar.Models
{
    public class BookListViewModel
    {
         public PagingInfo PagingInfo { get; set; }
         public IEnumerable<Book> Book { get; set; }
        public string CurrentGenre { get; set; }
    }
}
