using MyMovieApp.ViewModel;

namespace MyMovieApp.Interface
{
    public interface IMovieRepository
    {
        MovieViewModel Get(int id, string city);
        Task<List<MovieViewModel>> GetAll();
        Task<object> AddMovie(MovieViewModel movieViewModel);

        Task<object> UpdateMovie(MovieViewModel movieViewModel);
        Task<bool> DeleteMovie(int id);

        Task<HomeMovieViewModel> GetMoviesWithFilterytypes();
        Task<List<MovieViewModel>> FetchFilteredMovies(List<int> genre, string format, int pageNumber ,int PageSize, int language,  int sort, bool topSeller);
    }
}
