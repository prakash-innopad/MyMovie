namespace MyMovieApp.Models
    {
    public class Cinema
        {
        public int CinemaId { get; set; }

        public string Name { get; set; }

        public int AddressId { get; set; }
        public Address Address { get; set; }

        public ICollection<MovieCinema> MovieCinemas { get; set; }
        

        }
    }
