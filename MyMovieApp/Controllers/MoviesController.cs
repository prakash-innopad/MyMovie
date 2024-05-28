using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyMovieApp.Interface;
using MyMovieApp.ViewModel;

namespace MyMovieApp.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public MoviesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;   
        }
        public async Task<IActionResult> Index()
        {
            var result = await _unitOfWork.moviesRepository.GetAll();
            return View(result);
        }

        public ActionResult Details(int id)
        {
            var movie = _unitOfWork.moviesRepository.Get((int)id, "Ahmedabad");
            return View(movie);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MovieViewModel movieViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(movieViewModel);
            }
            try
            {

                var result = _unitOfWork.moviesRepository.AddMovie(movieViewModel);               
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var movie = _unitOfWork.moviesRepository.Get((int)id, "Ahemedabad");
            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(MovieViewModel movieViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result =  await _unitOfWork.moviesRepository.UpdateMovie(movieViewModel);
                    if ((bool)result)
                        TempData["success"] = "Movie updated successfully.";
                    else
                        TempData["error"] = "Error while updating movie.";
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int? id)
        {
           
            var movie = _unitOfWork.moviesRepository.Get((int)id, "Ahemedabad");
            if (movie == null)
            {
                return NotFound();
            }
            return View( movie);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var result = await _unitOfWork.moviesRepository.DeleteMovie(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
