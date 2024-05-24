using MyMovieApp.Interface;
using MyMovieApp.Models;

namespace MyMovieApp.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MyMovieContext _dbContext;
        public UnitOfWork(MyMovieContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IMovieRepository moviesRepository =>  new MovieRepository(_dbContext);
    }
}
