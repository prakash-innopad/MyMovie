using Microsoft.AspNetCore.Mvc.Rendering;
using MyMovieApp.ViewModel;

namespace MyMovieApp.Interface
{
    public interface IMovieRepository
    {
        Task<MovieViewModel> Get(int id, string city);
        Task<List<MovieViewModel>> GetAll();
        Task<object> AddMovie(MovieUpsertModel movieViewModel);

        Task<object> UpdateMovie(MovieUpsertModel movieViewModel);
        Task<bool> DeleteMovie(int id);

        Task<HomeMovieViewModel> GetMoviesWithFilterytypes();
        Task<List<MovieViewModel>> FetchFilteredMovies(List<int> genre, string format, int pageNumber ,int PageSize, int language,  int sort, bool topSeller);

        public Task<MovieUpsertModel> GetUpsertModel();
    }
}
