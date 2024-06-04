using BookStore.BL.Interfaces;
using BookStore.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BookStore.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBookService _bookService;

		public HomeController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var vm = new HomeIndexViewModel();
            vm.SpecialOfferBooksBooks = _bookService.GetSpecialOfferBooks();
            vm.FeaturedBooks= _bookService.GetFeaturedBooks();
            vm.BillBoardBooks = _bookService.GetBooks(5, 1);
            vm.BestSelling = _bookService.GetBooks(5, 1).FirstOrDefault();
			return View(vm);
        }

        [HttpGet]
        public IActionResult BookDetails(string pubId)
        {
            if(ModelState.IsValid)
            {
                var book = _bookService.GetBook(pubId);
                if(book != null)
                    return View(book);
            }
            return View("PageNotFound404");
        }

        [HttpGet]
        public IActionResult Cart()
        {
            return View("Cart");
        }

        [HttpGet]
        public IActionResult BookListing(string? pubId)
        {
            var vm = new BookListViewModel();
            vm.BookTypes = _bookService.GetBookTypes();
            if(string.IsNullOrEmpty(pubId))
            {
                vm.Books = _bookService.GetAllBooks();
            }
            else
            {
                var catid = _bookService.GetBookType(pubId).BookTypeID;
                vm.Books = _bookService.GetAllBooks().Where(x=>x.BookTypeId == catid).ToList();
            }
            return View("BookListing", vm);
        }

        [HttpPost]
        public void LikeBook(string pubId)
        {
            _bookService.LikeBook(pubId);
        }
        
        [HttpGet]
        public int BookLikes(string pubId)
        {
            return _bookService.BookLikes(pubId);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
