using BookStore.BL.Interfaces;
using BookStore.DAL.Models;
using BookStore.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookStore.Web.Controllers
{
	[Authorize(Roles = "Admin")]
	public class AdminController : Controller
	{
		private readonly IBookService _bookService;

		public AdminController(IBookService bookService)
		{
			_bookService = bookService;
		}

		[HttpGet]
		public IActionResult Index()
		{
			var vm = new AdminIndexViewModel();
			vm.BookCount = _bookService.GetBookCount();
			return View(vm);
		} 

		[HttpGet]
		public IActionResult Register()
		{
			var vm = new AddBookViewModel();
			vm.Types = _bookService.GetBookTypes().Select(x => new SelectListItem() { Value = x.PublicId, Text = x.TypeName });
			if (vm.Types.Count() == 0)
				return RedirectToAction("RegisterBookType");
			return PartialView("Add", vm);
		}

		[HttpPost]
		public IActionResult Register(AddBookViewModel bVm)
		{
			if (ModelState.IsValid)
			{
				_bookService.AddBook(bVm.Book, bVm.ChosedBookType);
				return RedirectToAction("BookList");
			}
            bVm.Types = _bookService.GetBookTypes().Select(x => new SelectListItem() { Value = x.PublicId, Text = x.TypeName });
            return View("Add", bVm);
		}


		[HttpGet]
		public IActionResult RegisterBookType()
		{
			return View("AddBookType");
		}
		//Edit-----------------------------------------------
		[HttpGet]
		public IActionResult Edit(string bookId)
		{
			if (ModelState.IsValid)
			{
				var book = _bookService.GetBook(bookId);
				if (book != null)
					return PartialView("Update", book);
			}
			return View("PageNotFound404");
		}

		[HttpPost]
		public IActionResult Update(Book book)
		{
			if (ModelState.IsValid)
			{
				_bookService.UpdateBook(book);
				return RedirectToAction("BookList");
			}
			return PartialView("Update", book);
		}
		//Edit-----------------------------------------------

		[HttpGet]
		public IActionResult EditBookType(string bookTypeId)
		{
			if (ModelState.IsValid)
			{
				var bType = _bookService.GetBookType(bookTypeId);
				if (bType != null)
					return View("UpdateBookType", bType);
			}
			return View("PageNotFound404");
		}


		[HttpGet]
		public IActionResult BookList()
		{
			var books = _bookService.GetAllBooks();
			return View(books);
		}

		[HttpGet]
		[Authorize(Roles = "Admin, Manager")]
		public IActionResult BookTypesList()
		{
			var types = _bookService.GetBookTypes();
			return View(types);
		}

		[HttpPost]
		public IActionResult UpdateBookType(BookType BType)
		{
			if (ModelState.IsValid)
			{
				_bookService.UpdateBookType(BType);
				return RedirectToAction("BookTypesList");
			}
			return View("EditBookType", BType.PublicId);
		}


		[HttpPost]
		public IActionResult RegisterBookType(BookType bType)
		{
			if (ModelState.IsValid)
			{
				_bookService.AddBookType(bType);
				return RedirectToAction("BookTypesList");
			}
			return View("AddBookType", bType);
		}

		[HttpPost]
		public IActionResult Delete(string id)
		{
			if (ModelState.IsValid)
			{
				var bType = _bookService.GetBook(id);
				if (bType != null)
					_bookService.DeleteBook(bType.ID);
				return RedirectToAction("BookList");
			}
			return View();
		}
		[HttpPost]
		public IActionResult DeleteBookType(string id)
		{
			if (ModelState.IsValid)
			{
				var ToDelete = _bookService.GetBookType(id);
				_bookService.DeleteBookType(ToDelete.BookTypeID);
				return RedirectToAction("BookTypesList");
			}
			return View();
		}
	}
}
