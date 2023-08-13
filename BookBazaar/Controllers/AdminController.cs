using Data.Entities;
using Data.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookBazaar.Controllers;

public class AdminController : Controller
{
    IBookRepository _bookRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly IOrderedBookRepository _orderedBookRepository;

    public AdminController(IBookRepository bookRepository, IOrderRepository orderRepository, IOrderedBookRepository orderedBookRepository)
    {
        _bookRepository = bookRepository;
        _orderRepository = orderRepository;
        _orderedBookRepository = orderedBookRepository;
    }

    public ActionResult Index()
    {
        return RedirectToAction("BookList");
    }

    public ViewResult BookList()
    {
        return View(_bookRepository.GetBook);
    }

    public ViewResult OrderList()
    {
        return View(_orderRepository.GetOrders);
    }

    public ViewResult OrderedBookList(Guid id)
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
            TempData["message"] = string.Format($"Товар \"{book.Name}\" збережено");
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
            TempData["message"] = string.Format($"Зміни інформації про товар \"{book.Name}\" збережено");
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

