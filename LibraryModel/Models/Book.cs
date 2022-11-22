using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace Butean_Rares_Lab2.Models
{
    public class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; }

        [Column(TypeName = "decimal(6, 2)")]
        public decimal Price { get; set; }
        public Author? Author { get; set; }
        public int? AuthorId { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<PublishedBook> PublishedBooks { get; set; }
    }
}
