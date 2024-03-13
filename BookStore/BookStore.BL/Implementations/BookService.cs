using BookStore.BL.Interfaces;
using BookStore.DAL;
using BookStore.DAL.Models;

namespace BookStore.BL.Implementations
{
    public class BookService : IBookService
    {
        private readonly ApplicationDbContext _appDBContext;
        private readonly IImageService _imageService;

        public BookService(ApplicationDbContext appDBContext, IImageService imageService)
        {
            _appDBContext = appDBContext;
            _imageService = imageService;
        }
        public void AddBook(Book ToAdd)
        {
            var resp = _imageService.AddPhoto(ToAdd.FormFile);
            ToAdd.Path = resp.Url.ToString();
            ToAdd.PublicID = resp.PublicId;
            _appDBContext.Books.Add(ToAdd);
            _appDBContext.SaveChanges();
        }
        public List<Book> GetAllBooks()
        {
            return _appDBContext.Books.ToList();
        }
        public Book GetBook(Guid id)
        {
            var book = _appDBContext.Books.Where(x => x.ID == id).FirstOrDefault();
            return book!;
        }
        public void DeleteBook(Guid id)
        {
            var book = _appDBContext.Books.Where(x => x.ID == id).FirstOrDefault()!;
            if (book == null)
                throw new Exception("Book was not found");
            _imageService.DeletePhoto(book.PublicID);
            _appDBContext.Books.Remove(book);
            _appDBContext.SaveChanges();
        }
        public void UpdateBook(Book book)
        {
            var bookToUpdate = _appDBContext.Books.FirstOrDefault(x => x.ID == book.ID);
            if (bookToUpdate == null)
                throw new Exception("Book was not found");
            bookToUpdate.Title = book.Title;
            bookToUpdate.Author = book.Author;
            bookToUpdate.Editura = book.Editura;
            bookToUpdate.Price = book.Price;
            bookToUpdate.Description = book.Description;
            if(book.FormFile != null)
            {
                _imageService.DeletePhoto(bookToUpdate.PublicID);
                var resp = _imageService.AddPhoto(book.FormFile);
                bookToUpdate.Path = resp.Url.ToString();
                bookToUpdate.PublicID = resp.PublicId;
            }
            _appDBContext.SaveChanges();
        }
    }
}
