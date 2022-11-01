namespace Butean_Rares_Lab2.Models
{
    public class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public Author? Author { get; set; }
        public int? AuthorId { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
