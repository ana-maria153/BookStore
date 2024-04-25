using BookStore.BL.Interfaces;
using BookStore.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BookStore.Web.Controllers;

public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;
    private readonly IBookService _bookService;

    public OrderController(IOrderService orderService, IBookService bookService)
    {
        _orderService = orderService;
        _bookService = bookService;
    }

    [HttpPost]
    public IActionResult ReceiveOrder([FromBody]OrderRequestViewModel order)
    {
        _orderService.PlaceOrder(order.OrderID, order.Name, order.Surname, order.Address, order.Phone, order.OrderProducts.Select(x => new Tuple<string, int>(x.ProdId, x.Quantity)).ToList());
        return Ok();
    }
    [HttpGet]
    public OrderRequestViewModel ReceiveOrder()
    {
        return new OrderRequestViewModel()
        {
            Name = "Dora",
            Surname = "Fax",
            Phone = "068841556",
            Address = "Chisinau, Rascani, Studentilor 5",
            OrderID = Guid.NewGuid(),
            OrderProducts = new List<OrderProductModel>()
            {
                new OrderProductModel()
                {
                    ProdId = "SomeProdID1",
                    Quantity = 10
                },
                new OrderProductModel()
                {
                    ProdId = "SomeProdID2",
                    Quantity = 3
                }
            }
        };
    }


    [HttpGet]
    public CartBookDetailsViewModel? CartBookDetail(string pubID)
    {
        var book = _bookService.GetBook(pubID);
        if (book == null)
            return null;
        return new CartBookDetailsViewModel { ImgSrc = book.Path, Author = book.Author, BookName = book.Title, PubId = pubID, Price = book.Price };
    }
}
