using Butean_Rares_Lab2.Models;
using Microsoft.EntityFrameworkCore;
using static NuGet.Packaging.PackagingConstants;
using static System.Reflection.Metadata.BlobBuilder;
using System.Runtime.ConstrainedExecution;
using System.Security.Policy;
using Publisher = Butean_Rares_Lab2.Models.Publisher;

namespace Butean_Rares_Lab2.Data
{
    public static class DbInitializer
    {
        //test
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new LibraryContext(serviceProvider.GetRequiredService<DbContextOptions<LibraryContext>>()))
            {
                context.Database.EnsureCreated();

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
                        }
                    };

                if (!context.Authors.Any())
                {
                    context.Authors.AddRange(authors);
                    context.SaveChanges();
                }

                if (!context.Books.Any())
                {
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
                       },
                       new Book
                       {
                           Title = "Fata de hartie",
                           Author = authors[0],
                           Price = Decimal.Parse("22")
                       },

                       new Book
                       {
                           Title = "Panza de paianjen",
                           Author = authors[1],
                           Price = Decimal.Parse("18")
                       },

                       new Book
                       {
                           Title = "De veghe in lanul desecara",
                           Author = authors[2],
                           Price = Decimal.Parse("27")
                       }
                   );
                    context.SaveChanges();
                }

                if (!context.Customers.Any())
                {
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

                if (!context.Orders.Any())
                {
                    context.Orders.AddRange(new List<Order>
                    {
                        new Order{BookId=context.Books.Skip(0).First().BookId,CustomerId=context.Customers.Single(c=>c.Name=="Popescu Marcela").CustomerId,OrderDate=DateTime.Parse("2021-02-25")},
                        new Order{BookId=context.Books.Skip(1).First().BookId,CustomerId=context.Customers.Single(c=>c.Name=="Popescu Marcela").CustomerId,OrderDate=DateTime.Parse("2021-09-28")},
                        new Order{BookId=context.Books.Skip(2).First().BookId,CustomerId=context.Customers.Single(c=>c.Name=="Popescu Marcela").CustomerId,OrderDate=DateTime.Parse("2021-10-28")},
                        new Order{BookId=context.Books.Skip(3).First().BookId,CustomerId=context.Customers.Single(c=>c.Name=="Mihailescu Cornel").CustomerId,OrderDate=DateTime.Parse("2021-09-28")},
                        new Order{BookId=context.Books.Skip(4).First().BookId,CustomerId=context.Customers.Single(c=>c.Name=="Mihailescu Cornel").CustomerId,OrderDate=DateTime.Parse("2021-09-28")},
                        new Order{BookId=context.Books.Skip(5).First().BookId,CustomerId=context.Customers.Single(c=>c.Name=="Mihailescu Cornel").CustomerId,OrderDate=DateTime.Parse("2021-10-28")}
                    });
                    context.SaveChanges();
                }

                if (!context.Publishers.Any())
                {
                    context.AddRange(new List<Publisher>
                    {
                        new Publisher{PublisherName="Humanitas",Adress="Str. Aviatorilor, nr. 40,Bucuresti"},
                        new Publisher{PublisherName="Nemira",Adress="Str. Plopilor, nr. 35,Ploiesti"},
                        new Publisher{PublisherName="Paralela 45",Adress="Str. Cascadelor, nr.22, Cluj-Napoca"},
                    });
                    context.SaveChanges();
                }

                if (!context.PublishedBooks.Any())
                {
                    context.PublishedBooks.AddRange(new List<PublishedBook>
                    {
                        //new PublishedBook {BookId = context.Books.Single(c => c.Title == "Maytrei" ).BookId, PublisherId = context.Publishers.Single(i => i.PublisherName =="Humanitas").ID},
                        //new PublishedBook {BookId = context.Books.Single(c => c.Title == "Enigma Otiliei" ).BookId, PublisherId = context.Publishers.Single(i => i.PublisherName =="Humanitas").ID},
                        //new PublishedBook {BookId = context.Books.Single(c => c.Title == "Baltagul" ).BookId,PublisherId = context.Publishers.Single(i => i.PublisherName =="Nemira").ID},
                        //new PublishedBook {BookId = context.Books.Single(c => c.Title == "Fata de hartie" ).BookId,PublisherId = context.Publishers.Single(i => i.PublisherName == "Paralela 45").ID},
                        //new PublishedBook {BookId = context.Books.Single(c => c.Title == "Panza de paianjen" ).BookId,PublisherId = context.Publishers.Single(i => i.PublisherName == "Paralela 45").ID},
                        new PublishedBook {BookId = context.Books.Single(c => c.Title == "De veghe in lanul desecara" ).BookId,PublisherId = context.Publishers.Single(i => i.PublisherName == "Paralela 45").PublisherId}
                    });
                    context.SaveChanges();
                }
            }
        }
    }
}
