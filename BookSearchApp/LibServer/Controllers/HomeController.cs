using Microsoft.AspNetCore.Mvc;
using LibServer.Models;

namespace LibServer.Controllers
{
    public class HomeController : Controller
    {
        public ViewResult Index()
        {
            var genres = LibCore.Genre.Genres.Select(g => g.Name).OrderBy(s => s).ToList();
            return View(genres);
        }

        [HttpPost]
        public ViewResult Books(RetrievalBook rb)
        {
            var rbd = new LibCore.RetrievalBook
            {
                Title = rb.Title ?? string.Empty,
                Author = rb.Author ?? string.Empty,
                Publisher = rb.Publisher ?? string.Empty,
                Genre = rb.Genre ?? string.Empty,
                Grade = LibCore.BookGrade.None
            };

            if (rb.Grade == "excelent") rbd.Grade |= LibCore.BookGrade.Excelent;
            if (rb.Grade == "good") rbd.Grade |= LibCore.BookGrade.Good;
            if (rb.Grade == "neutral") rbd.Grade |= LibCore.BookGrade.Neutral;
            if (rb.Grade == "bad") rbd.Grade |= LibCore.BookGrade.Bad;
            if (rb.Grade == "worst") rbd.Grade |= LibCore.BookGrade.Worst;

            var list = LibCore.DataManager.GetBooks(rbd)
                .Select(b => new ModelBook(b))
                .OrderBy(b => b.Author).ThenBy(b => b.Title)
                .ToList();

            return View(list);
        }
    }
}