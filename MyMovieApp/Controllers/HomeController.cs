using Microsoft.AspNetCore.Mvc;
using MyMovieApp.Interface;
using MyMovieApp.Models;
using MyMovieApp.ViewModel;
using System.Diagnostics;
using System.Drawing.Printing;

namespace MyMovieApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public HomeController(ILogger<HomeController> logger,  IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }
        public IActionResult Blazor()
            {
            return View("_Host");
            }
        public async Task<IActionResult> Index()
        {
            var result = await _unitOfWork.moviesRepository.GetMoviesWithFilterytypes();
            return View(result);
        }
        [HttpPost]
        public async Task<IActionResult> GetFilteredMovies(List<int> genre, string format, string searchText, int page = 1, int size = 5, int lang = 0 ,int sort = 0,  bool top_sell = false)
        {
            var movies = await _unitOfWork.moviesRepository.FetchFilteredMovies(genre, format, searchText, page,size, lang, sort, top_sell);
            var paginatedMovieViewModel = new PaginatedMovieViewModel
            {
                Movies = movies,
                PageNumber = page,
                PageSize = size,
                TotalItems = movies.Count()
            };
            // return Json(paginatedMovieViewModel);
            return PartialView("_MoviesPartial", paginatedMovieViewModel);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}