using Data.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookBazaar.Controllers
{
    public class NavController : Controller
    {
        private IBookRepository repository;

        public NavController(IBookRepository repository)
        {
            this.repository = repository;
        }
        public PartialViewResult Menu(string genre)
        {
            ViewBag.SelectedCategory = genre;

            IEnumerable<string> genres = repository.GetBook
                .Select(p => p.Category)
                .Distinct()
                .OrderBy(x => x);

            return PartialView(genres);
        }
    }
}
