using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyMovieApp.Interface;
using MyMovieApp.ViewModel;

namespace MyMovieApp.Controllers
    {
    public class CinemaController : Controller
        {
        private readonly IUnitOfWork _unitOfWork;
        public CinemaController(IUnitOfWork unitOfWork)
            {
            _unitOfWork = unitOfWork;
            }

        public async Task<ActionResult> Index()
            {
            var result = await _unitOfWork.cinemaRepository.GetAll();
            return View(result);
            }


        public ActionResult Details(int id)
            {
            return View();
            }


        public ActionResult Create()
            {
            return View();
            }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CinemaViewModel cinemaViewModel)
            {
            if (!ModelState.IsValid)
                {

                return View(cinemaViewModel);
                }
            try
                {
                var result = await _unitOfWork.cinemaRepository.AddCinema(cinemaViewModel);
                if (result != null)
                    TempData["success"] = "Cinema Added successfully.";
                else
                    TempData["error"] = "Error while adding cinema.";
                return RedirectToAction(nameof(Index));
                }
            catch
                {
                return View();
                }
            }

        public async Task<ActionResult> Edit(int id)
            {
            if (id == null || id == 0)
                {
                return NotFound();
                }
            var cinema = await _unitOfWork.cinemaRepository.Get(id);

            if (cinema == null)
                {
                return NotFound();
                }
            return View(cinema);
            }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(CinemaViewModel cinemaViewModel)
            {
            try
                {
                if (ModelState.IsValid)
                    {
                    var result = await _unitOfWork.cinemaRepository.UpdateCinema(cinemaViewModel);
                    if ((bool)result)
                        TempData["success"] = "Cinema updated successfully.";
                    else
                        TempData["error"] = "Error while updating movie.";
                    }
                return RedirectToAction(nameof(Index));
                }
            catch (Exception ex)
                {
                return View();
                }
            }


        [HttpDelete]
        //  [ValidateAntiForgeryToken]
        public async Task<bool> Delete(int id)
            {
            try
                {
                var result = await _unitOfWork.cinemaRepository.DeleteCinema(id);
                if ((bool)result)
                    TempData["success"] = "Cinema Deleted successfully.";
                else
                    TempData["error"] = "Error while deleting Cinema.";
                 return result;
                }
            
            catch
                {
                return false;
                }
            }
    }
    }
