using BookStore.BL.Interfaces;
using BookStore.DAL;
using BookStore.DAL.Models;
using Microsoft.EntityFrameworkCore;

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
            return _appDBContext.Books.Select(x=>x).Include(x=>x.BookType).ToList();
        }

        public List<Book> GetBooks(int count, int page)
        {
            return _appDBContext.Books.Skip(count*(page-1)).Take(count).ToList();
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
            bookToUpdate.BookTypeId = book.BookTypeId;
            if(book.FormFile != null)
            {
                _imageService.DeletePhoto(bookToUpdate.PublicID);
                var resp = _imageService.AddPhoto(book.FormFile);
                bookToUpdate.Path = resp.Url.ToString();
                bookToUpdate.PublicID = resp.PublicId;
            }
            _appDBContext.SaveChanges();
        }

        public Book GetBook(string pubID)
        {
            var book = _appDBContext.Books.Where(x => x.PublicID == pubID).FirstOrDefault();
            return book!;
        }

        public void AddBookType(BookType bType)
        {
            bType.PublicId = Guid.NewGuid().ToString().Substring(0, 5);
            _appDBContext.BookTypes.Add(bType);
            _appDBContext.SaveChanges();
        }

        public void UpdateBookType(BookType bType)
        {
            var bookTypeToUpdate = _appDBContext.BookTypes.FirstOrDefault(x => x.BookTypeID == bType.BookTypeID);
            if (bookTypeToUpdate == null)
                throw new Exception("BookType was not found");
            bookTypeToUpdate.TypeName = bType.TypeName;
            bookTypeToUpdate.TypeDescription = bType.TypeDescription;
            _appDBContext.SaveChanges();
        }

        public void DeleteBookType(Guid id)
        {
            var bookTypeToDelete = _appDBContext.BookTypes.FirstOrDefault(x => x.BookTypeID == id);
            if (bookTypeToDelete == null)
                throw new Exception("BookType was not found");
            _appDBContext.BookTypes.Remove(bookTypeToDelete);
            _appDBContext.SaveChanges();
        }

        public List<BookType> GetBookTypes()
        {
            var types = _appDBContext.BookTypes.ToList();
            return types;
        }

        public BookType GetBookType(Guid id)
        {
            var bType = _appDBContext.BookTypes.FirstOrDefault(x=>x.BookTypeID == id);
            return bType;
        }

        public void LikeBook(string PubId)
        {
            var BookToBeLiked = _appDBContext.Books.FirstOrDefault(x => x.PublicID == PubId);
            if(BookToBeLiked == null)
                throw new Exception("Book was not found");
            BookToBeLiked.BookLikes++;
            _appDBContext.SaveChanges();
        }

        public BookType GetBookType(string id)
        {
            var bType = _appDBContext.BookTypes.FirstOrDefault(x => x.PublicId == id);
            return bType;
        }

        public void AddBook(Book ToAdd, string BTID)
        {
            var BType = GetBookType(BTID);
            if (BType == null)
                throw new Exception("Book Type was not found");
            ToAdd.BookTypeId = BType.BookTypeID;
            AddBook(ToAdd);
        }

        public List<Book> GetBooksByType(string id)
        {
            var bookType = GetBookType(id);
            var books = _appDBContext.Books.Where(x => x.BookTypeId == bookType.BookTypeID).ToList();
            return books;
        }

		public List<Book> GetFeaturedBooks()
		{
			return _appDBContext.Books.OrderBy(x => Guid.NewGuid()).Take(4).ToList();
		}

		public List<Book> GetSpecialOfferBooks()
		{
			return _appDBContext.Books.OrderBy(x => x.Price).Take(4).ToList();
		}

        public int GetBookCount()
        {
            return _appDBContext.Books.Count();
        }

        public int BookLikes(string PubId)
        {
            var bookLikes = _appDBContext.Books.FirstOrDefault(x => x.PublicID == PubId).BookLikes;
            return bookLikes;
        }
    }
}
