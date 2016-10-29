using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BoardGames.Search;

namespace BoardGames.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISearchIndexBuilder _searchIndexBuilder;

        public HomeController(ISearchIndexBuilder searchIndexBuilder)
        {
            _searchIndexBuilder = searchIndexBuilder;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult RebuildIndexes()
        {
            _searchIndexBuilder.RebuildIndexes();

            return Content("Success!");
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
