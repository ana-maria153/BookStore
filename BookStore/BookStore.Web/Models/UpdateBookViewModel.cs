using BookStore.DAL.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookStore.Web.Models;

public class UpdateBookViewModel
{
    public Book Book { get; set; }
    public IEnumerable<SelectListItem> Types { get; set; }

    public string? ChosedBookType { get; set; } = "";
    public UpdateBookViewModel()
    {
        Types = new List<SelectListItem>();
        Book = new Book();
    }
}
