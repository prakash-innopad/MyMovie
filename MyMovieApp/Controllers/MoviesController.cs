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

        public async Task<ActionResult> Details(int id)
        {
            var movie = await _unitOfWork.moviesRepository.Get((int)id, "Ahmedabad");
            return View(movie);
        }

        public async Task<ActionResult> Create()
        {
            var model = await _unitOfWork.moviesRepository.GetUpsertModel();           
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(MovieUpsertModel movieViewModel)
        {
            if (!ModelState.IsValid)
            {
                var model = await _unitOfWork.moviesRepository.GetUpsertModel();
                return View(model);
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

        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var movie = await _unitOfWork.moviesRepository.Get((int)id, "Ahemedabad");
            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(MovieUpsertModel movieViewModel)
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

        public async Task<ActionResult> Delete(int? id)
        {
           
            var movie = await _unitOfWork.moviesRepository.Get((int)id, "Ahemedabad");
            if (movie == null)
            {
                return NotFound();
            }
            return View( movie);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [HttpPost, ActionName("Delete")]
        public async Task<ActionResult> DeleteMovie(int id)
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
