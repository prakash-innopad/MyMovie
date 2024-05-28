using System.ComponentModel.DataAnnotations.Schema;

namespace MyMovieApp.Models
    {
    public class MovieCinema
        {
        public int Id { get; set; }

        [ForeignKey(nameof(Movie))]
        public int MovieId {  get; set; }
        public Movie Movie { get; set; }

        [ForeignKey(nameof(Cinema))]
        public int CinemaId {  get; set; }
        public Cinema Cinema { get; set; }

        public ICollection<ShowDetail> ShowDetails { get; set; }

        }
    }
