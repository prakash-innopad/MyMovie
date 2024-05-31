using MyMovieApp.ViewModel;

namespace MyMovieApp.Interface
    {
    public interface ICinemaRepository
        {
        Task<CinemaViewModel> Get(int id);
        Task<List<CinemaViewModel>> GetAll();
         Task<object> AddCinema(CinemaViewModel model);
        }
    }
