using BookStore.BL.Interfaces;
using BookStore.DAL.Models;
using BookStore.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookStore.Web.Controllers
{
    public class AdminController : Controller
    {
        private readonly IBookService _bookService;
        private readonly IOrderService _orderService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminController(IBookService bookService, IOrderService orderService, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _bookService = bookService;
            _orderService = orderService;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var vm = new AdminIndexViewModel();
            vm.BookCount = _bookService.GetBookCount();
            return View(vm);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Register()
        {
            var vm = new AddBookViewModel();
            vm.Types = _bookService.GetBookTypes().Select(x => new SelectListItem() { Value = x.PublicId, Text = x.TypeName });
            if (vm.Types.Count() == 0)
                return RedirectToAction("RegisterBookType");
            return PartialView("Add", vm);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        public IActionResult RegisterBookType()
        {
            return View("AddBookType");
        }

        [HttpGet]
        [Authorize]
        public IActionResult Orders()
        {
            var vm = new OrderListViewModel();
            vm.Orders = _orderService.GetOrders();
            return View("Orders", vm);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Details(Guid id)
        {
            var order = _orderService.GetOrder(id);
            return View("Details", order);
        }
        //Edit-----------------------------------------------
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(string bookId)
        {
            if (ModelState.IsValid)
            {
                var vm = new UpdateBookViewModel();
                vm.Types = _bookService.GetBookTypes().Select(x => new SelectListItem() { Value = x.PublicId, Text = x.TypeName });
                vm.Book = _bookService.GetBook(bookId);
                if (vm.Types.Count() == 0)
                    return RedirectToAction("RegisterBookType");
                return PartialView("Update", vm);
            }
            return View("PageNotFound404");
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Update([FromForm] UpdateBookViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var btype = _bookService.GetBookType(vm.ChosedBookType);
                if(!string.IsNullOrEmpty(vm.ChosedBookType))
                    vm.Book.BookTypeId = btype.BookTypeID;
                _bookService.UpdateBook(vm.Book);
                return RedirectToAction("BookList");
            }
            vm.Types = _bookService.GetBookTypes().Select(x => new SelectListItem() { Value = x.PublicId, Text = x.TypeName });
            return PartialView("Update", vm);
        }
        //Edit-----------------------------------------------

        [HttpGet]
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        public IActionResult BookList()
        {
            var books = _bookService.GetAllBooks();
            return View(books);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult BookTypesList()
        {
            var types = _bookService.GetBookTypes();
            return View(types);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult UpdateBookType(BookType BType)
        {
            var toUpdate = _bookService.GetBookType(BType.PublicId);
            BType.BookTypeID = toUpdate.BookTypeID;
            if (ModelState.IsValid)
            {
                _bookService.UpdateBookType(BType);
                return RedirectToAction("BookTypesList");
            }
            return View("UpdateBookType", BType);
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult AdminUserRoles()
        {
            var users = _userManager.Users.ToList();
            ViewBag.Roles = _roleManager.Roles.ToList();
            return View("AdminUserRoles", users);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateUserRole([FromBody] UpdateUserRoleViewModel update)
        {
            var user = await _userManager.FindByIdAsync(update.userId);
            if (user == null)
                return NotFound();
            var role = await _roleManager.FindByIdAsync(update.newRoleId);
            if (role == null)
                return NotFound();
            var userRoles = await _userManager.GetRolesAsync(user);
            var result = await _userManager.RemoveFromRolesAsync(user, userRoles);
            if (!result.Succeeded)
                return Ok(result);
            result = await _userManager.AddToRoleAsync(user, role.Name);
            if (!result.Succeeded)
                return BadRequest(result);
            return RedirectToAction("AdminUserRoles");
        }
    }
}
