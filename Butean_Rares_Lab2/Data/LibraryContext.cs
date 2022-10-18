using Butean_Rares_Lab2.Models;
using Microsoft.EntityFrameworkCore;

namespace Butean_Rares_Lab2.Data
{
    public class LibraryContext : DbContext
    {
        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options) { }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Book>().ToTable("Book");
            builder.Entity<Order>().ToTable("Order");
            builder.Entity<Customer>().ToTable("Customer");
            builder.Entity<Author>().ToTable("Author");
        }
    }
}
