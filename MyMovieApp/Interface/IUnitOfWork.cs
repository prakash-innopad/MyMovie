namespace MyMovieApp.Interface
{
    public interface IUnitOfWork
    {
        public IMovieRepository moviesRepository { get; }
    }
}
