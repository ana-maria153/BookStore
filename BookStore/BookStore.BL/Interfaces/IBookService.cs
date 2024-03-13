using BookStore.DAL.Models;

namespace BookStore.BL.Interfaces
{
    public interface IBookService
    {
        public void AddBook(Book ToAdd);
        public List<Book> GetAllBooks();
        public Book GetBook(Guid id);
        public void UpdateBook(Book book);
        public void DeleteBook(Guid id);
    }
}
