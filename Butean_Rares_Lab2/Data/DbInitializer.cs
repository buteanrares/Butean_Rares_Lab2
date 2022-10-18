using Butean_Rares_Lab2.Models;
using Microsoft.EntityFrameworkCore;

namespace Butean_Rares_Lab2.Data
{
    public static class DbInitializer
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new
           LibraryContext(serviceProvider.GetRequiredService
            <DbContextOptions<LibraryContext>>()))
            {
                foreach (var entity in context.Books)
                {
                    context.Books.Remove(entity);
                }

                var authors = new List<Author>() {
                    new Author
                    {
                        FirstName="Mihail",
                        LastName="Sadoveanu"
                    },
                    new Author
                    {
                        FirstName="George",
                        LastName="Calinescu"
                    },
                    new Author
                    {
                        FirstName="Mircea",
                        LastName="Eliade"
                    },
                };
                context.Authors.AddRange(authors);
                context.Books.AddRange(
                    new Book
                    {
                        Title = "Baltagul",
                        Author = authors[0],
                        Price = Decimal.Parse("22")
                    },

                    new Book
                    {
                        Title = "Enigma Otiliei",
                        Author = authors[1],
                        Price = Decimal.Parse("18")
                    },

                    new Book
                    {
                        Title = "Maytrei",
                        Author = authors[2],
                        Price = Decimal.Parse("27")
                    }
                );


                context.Customers.AddRange(
                new Customer
                {
                    Name = "Popescu Marcela",
                    Address = "Str. Plopilor, nr. 24",
                    BirthDate = DateTime.Parse("1979-09-01")
                },
                new Customer
                {
                    Name = "Mihailescu Cornel",
                    Address = "Str. Bucuresti, nr. 45, ap. 2",
                    BirthDate = DateTime.Parse("1969 - 07 - 08")
                }

                );

                context.SaveChanges();
            }
        }
    }
}
