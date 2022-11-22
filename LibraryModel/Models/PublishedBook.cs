namespace Butean_Rares_Lab2.Models
{
    public class PublishedBook
    {
        public int PublisherId { get; set; }
        public int BookId { get; set; }
        public Publisher Publisher { get; set; }
        public Book Book { get; set; }
    }
}
