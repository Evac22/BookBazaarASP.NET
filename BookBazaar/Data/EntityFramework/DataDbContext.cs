using Microsoft.EntityFrameworkCore;
using Data.Entities;
using Data.Interfaces;

namespace Data.EntityFramework
{
    public class DataDbContext : DbContext, IDataDbContext 
    {
        public DataDbContext(DbContextOptions<DataDbContext> options) : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderedBook> OrderedBooks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
                .HasKey(b => b.Id);

            modelBuilder.Entity<Order>()
                .HasKey(o => o.Id);

            modelBuilder.Entity<OrderedBook>()
                .HasKey(ob => new {ob.OrderId, ob.BookId});

            modelBuilder.Entity<OrderedBook>()
                .HasOne(ob => ob.Order)
                .WithMany(o => o.OrderedBook)
                .HasForeignKey(ob => ob.OrderId);

            modelBuilder.Entity<OrderedBook>()
                .HasOne(ob => ob.Book)
                .WithMany()
                .HasForeignKey(of => of.BookId);
        }
    }

}
