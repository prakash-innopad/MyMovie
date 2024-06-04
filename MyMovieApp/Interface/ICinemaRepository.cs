using MyMovieApp.ViewModel;

namespace MyMovieApp.Interface
    {
    public interface ICinemaRepository
        {
        Task<CinemaViewModel> Get(int id);
        Task<List<CinemaViewModel>> GetAll();
         Task<object> AddCinema(CinemaViewModel cinema);
        Task<bool> UpdateCinema(CinemaViewModel cinema);

        Task<bool> DeleteCinema(int id);
        }
    }
