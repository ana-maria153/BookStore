using BookStore.DAL.Models;

namespace BookStore.BL.Interfaces
{
    public interface IBookService
    {
        public void AddBook(Book ToAdd);
        public void AddBook(Book ToAdd, string BTID);
        public List<Book> GetAllBooks();
        public List<Book> GetBooks(int count, int page);
        public Book GetBook(Guid id);
        public Book GetBook(string pubID);
        public void UpdateBook(Book book);
        public void DeleteBook(Guid id);

        //book type
        public void AddBookType(BookType bType);
        public void UpdateBookType(BookType bType);
        public void DeleteBookType(Guid id);
        public List<BookType> GetBookTypes();
        public BookType GetBookType(Guid id);
        public BookType GetBookType(string id);
        public List<Book> GetBooksByType(string id);

        //others
        public void LikeBook(string PubId);
        public int BookLikes(string PubId);
        public List<Book> GetFeaturedBooks();
        public List<Book> GetSpecialOfferBooks();
        public int GetBookCount();
    }
}
