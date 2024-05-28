namespace MyMovieApp.Models
    {
    public class ShowDetail
        {
        public int Id { get; set; }
        public int MovieCinemaId {  get; set; }

        public MovieCinema MovieCinema { get; set; }

        public DateTime ShowTime { get; set; }
        public int ScreenNumber { get; set; }
        }
    }
