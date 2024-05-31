using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyMovieApp.Models
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }

        public string? Title { get; set; }

        public DateTime? ReleaseDate { get; set; }

        public string? ImageLink { get; set; }

        public string? TrailerLink { get; set; }

        public TimeSpan Runtime { get; set; }
        public string Synopsis { get; set; }

        public int Likes { get; set; }

        public int DisLikes { get; set; }
        public string? Format { get; set; }

        [ForeignKey(nameof(Certificate))]
        public int? CertificateId { get; set; }       
        public Certificate? Certificate { get; set; }

        public ICollection<MovieLanguage> MovieLanguages { get; set; }

        public ICollection<MovieGenre>? MovieGenres { get; set; }
        public ICollection<Cast>? Casts { get; set; }
        public ICollection<MovieCinema>? MovieCinemas { get; set; }

        }
}
