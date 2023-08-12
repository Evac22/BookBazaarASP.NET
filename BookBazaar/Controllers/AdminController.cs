using Data.Entities;
using Data.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookBazaar.Controllers
{
    public class AdminController : Controller
    {
        IBookRepository _bookRepository;
        private readonly IOrderRepository _orderReository;
        private readonly IOrderedBookRepository _orderedBookRepository;

        public AdminController(IBookRepository bookRepository, IOrderRepository orderReository, IOrderedBookRepository orderedBookRepository)
        {
            _bookRepository = bookRepository;
            _orderReository = orderReository;
            _orderedBookRepository = orderedBookRepository;
        }

        public ActionResult Index()
        {
            return RedirectToAction("BookList");
        }

        public ActionResult BookList() 
        {
            return View(_bookRepository.GetBook);
        }

        public ViewResult OrderList()
        {
            return View(_orderReository.GetOrders);
        }

        public ViewResult OrderedFoodList(Guid id)
        {
            var orderedBook = _orderedBookRepository.GetOrderedBook.Where(p => p.OrderId == id).ToList();
            return View(orderedBook);
        }

        public ViewResult Add()
        {
            return View(new Book());
        }

        [HttpPost]
        public ActionResult Add(Book book)
        {
            if (ModelState.IsValid)
            {
                _bookRepository.Add(book);
                TempData["message"] = string.Format($"Товар \" {book.Name}\"запрещенно");
                return RedirectToAction("BookList");
            }
            else
            {
                return View(book);
            }
        }

        public ViewResult Edit(Guid id)
        {
            Book book = _bookRepository.GetBook.FirstOrDefault(p => p.Id == id);
            return View(book);
        }

        [HttpPost]
        public ActionResult Edit(Book book)
        {
            if (ModelState.IsValid)
            {
                _bookRepository.Update(book);
                TempData["message"] = string.Format($"Изменения информации о товаре \"{book.Name}\" cохраненно");
                return RedirectToAction("BookList");
            }
            else
            {
                return View(book);
            }
        }

        [HttpPost]
        public ActionResult Delete(Book book)
        {
            _bookRepository.Delete(book.Id);
            return RedirectToAction("BookList");
        }
    }
}
