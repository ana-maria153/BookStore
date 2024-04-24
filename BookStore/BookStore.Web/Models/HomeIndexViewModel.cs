using BookStore.DAL.Models;

namespace BookStore.Web.Models
{
	public class HomeIndexViewModel
	{
        public List<Book> FeaturedBooks { get; set; }
        public List<Book> SpecialOfferBooksBooks { get; set; }
        public List<Book> BillBoardBooks { get; set; }
        public Book BestSelling { get; set; }

        public HomeIndexViewModel()
        {
            FeaturedBooks = new List<Book>();
            SpecialOfferBooksBooks = new List<Book>();
            BillBoardBooks = new List<Book>();
            BestSelling = new Book();
        }
    }
}
