using Microsoft.AspNetCore.Mvc;
using BoardGames.Search;

namespace BoardGames.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISearchIndexBuilder _searchIndexBuilder;
        private readonly ISearcher _searcher;

        public HomeController(ISearchIndexBuilder searchIndexBuilder, ISearcher searcher)
        {
            _searchIndexBuilder = searchIndexBuilder;
            _searcher = searcher;
        }

        public IActionResult Index(SearchCriteria criteria)
        {
            if (criteria == null)
                criteria = new SearchCriteria();

            var results = _searcher.Search(criteria);

            return View(results);
        }

        [HttpPost]
        public IActionResult Search([FromBody] SearchCriteria criteria)
        {
            var results = _searcher.Search(criteria);

            return PartialView("_Search", results);
        }

        public IActionResult RebuildIndexes()
        {
            _searchIndexBuilder.RebuildIndexes();

            return Content("Success!");
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
