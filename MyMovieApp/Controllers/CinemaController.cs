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
        public ActionResult Create(CinemaViewModel cinemaViewModel)
            {
            if (!ModelState.IsValid)
                {
               
                return View(cinemaViewModel);
                }
            try
                {
                var result = _unitOfWork.cinemaRepository.AddCinema(cinemaViewModel);
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

        public ActionResult Edit(int id)
            {
            return View();
            }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
            {
            try
                {
                return RedirectToAction(nameof(Index));
                }
            catch
                {
                return View();
                }
            }


        public ActionResult Delete(int id)
            {
            return View();
            }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
            {
            try
                {
                return RedirectToAction(nameof(Index));
                }
            catch
                {
                return View();
                }
            }
        }
    }
