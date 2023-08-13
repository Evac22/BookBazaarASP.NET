using BookBazaar.Models;
using Data.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookBazaar.Controllers
{
    public class BookController : Controller
    {
        private IBookRepository _repository;
        public int pageSize = 5;
        public BookController(IBookRepository repository)
        {
            _repository = repository;
        }
        public ViewResult List(string category, int page = 1)
        {
            var model = new BookListViewModel
            {
                Book = _repository.GetBook
                    .Where(p => category == null || p.Category == category)
                    .OrderBy(book => book.Id)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = category == null ?
                        _repository.GetBook.Count() :
                        _repository.GetBook.Count(p => p.Category == category)
                },
                CurrentCategory = category
            };
            return View(model);
        }
    }
}
