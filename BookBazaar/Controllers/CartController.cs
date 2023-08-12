using BookBazaar.Models;
using Data.Entities;
using Data.Interfaces;
using Microsoft.AspNetCore.Mvc;
using ShippingDetails = BookBazaar.Models.ShippingDetails;

namespace BookBazaar.Controllers
{
    public class CartController : Controller
    {
        private const string cartSessionKey = "Cart";

        private readonly IBookRepository _bookRepository;
        private readonly IOrderRepository _orderRepository;
        public CartController(IBookRepository bookRepository, IOrderRepository orderRepository)
        {
            _bookRepository = bookRepository;
            _orderRepository = orderRepository;
        }
        public ActionResult Index(Cart cart , string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = cart ,
                ReturnUrl = returnUrl
            });
        }

        public RedirectToActionResult AddToCart(Cart cart, Guid id, string returnUrl)
        {
            Book Book = _bookRepository.GetBook
                .FirstOrDefault(p => p.Id == id);

            if (Book == null)
            {
                cart.AddItem(Book, 1);
            }

            HttpContext.Session.SetObject(cartSessionKey, cart);
            return RedirectToAction("Index", new {returnUrl});
        }

        public PartialViewResult Summary(Cart cart)
        {
            return PartialView(cart);
        }

        public ViewResult Checkout()
        {
            return View(new ShippingDetails());
        }

        [HttpPost]
        public ViewResult Checkout(Cart cart, ShippingDetails shippingDetails)
        {
            if (cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Извините, корзина пуста!");
            }
            if (ModelState.IsValid)
            {
                var order = new Order
                {
                    Address = shippingDetails.Address,
                    Email = shippingDetails.Email,
                    FirstName = shippingDetails.FirstName,
                    SecondName = shippingDetails.SecondName,
                    Phone = shippingDetails.Phone,
                    TotalPrice = cart.Lines.Sum(l => l.Book.Price * l.Quantity)
                };

                var orderedBook = cart.Lines.Select(x => new OrderedBook
                {
                    Order = order,
                    BookId = x.Book.Id,
                    Quantity = x.Quantity,
                    Id = Guid.NewGuid()
                });

                order.OrderedBook = orderedBook.ToArray();

                _orderRepository.Add(order);

                cart.Clear();
                HttpContext.Session.SetObject(cartSessionKey, cart);
                return View("Completed");
            }
            else
            {
                return View(new ShippingDetails());
            }

        }

    }
}
