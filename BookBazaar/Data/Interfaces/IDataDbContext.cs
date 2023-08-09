using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Interfaces
{
    public interface IDataDbContext 
    {
        DbSet<Book> Books { get; set; }
        DbSet<Order> Orders { get; set;}
        DbSet<OrderedBook> OrderedBooks { get; set; }
        int SaveChanges();
    }
}
