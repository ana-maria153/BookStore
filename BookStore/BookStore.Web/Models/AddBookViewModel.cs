using BookStore.BL.Interfaces;
using BookStore.DAL.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookStore.Web.Models
{
    public class AddBookViewModel
    {
        public Book Book { get; set; }
        public IEnumerable<SelectListItem> Types { get; set; }

        public string ChosedBookType { get; set; } = "";
        public AddBookViewModel()
        {
            Types = new List<SelectListItem>();
            Book = new Book();
        }
    }
}
