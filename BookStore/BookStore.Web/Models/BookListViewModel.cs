using BookStore.DAL.Models;

namespace BookStore.Web.Models;

public class BookListViewModel
{
    public List<BookType> BookTypes { get; set; }
    public List<Book> Books { get; set; }
}
