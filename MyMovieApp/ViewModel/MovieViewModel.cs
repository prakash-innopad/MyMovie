using MyMovieApp.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyMovieApp.ViewModel
{
    public class MovieViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter movie title.")]
        public string? Title { get; set; }

        [DisplayName("Release Date")]
        [DataType(DataType.Date)]
        public DateTime? ReleaseDate { get; set; }

        [DisplayName("Image Link")]
        public string? ImageLink { get; set; }
        [DisplayName("Trailer Link")]
        public string? TrailerLink { get; set; }
        public decimal Price { get; set; }
        [RegularExpression(@"^([0-1][0-9]|2[0-3]):[0-5][0-9]:[0-5][0-9](\.[0-9]{1,7})?$", ErrorMessage = "Runtime must be between 00:00:00.0000000 and 23:59:59.9999999.")]
        public TimeSpan Runtime { get; set; }
        public string Synopsis { get; set; }
        public int Likes { get; set; }
        public int DisLikes { get; set; }
        public string? Format { get; set; }
        public int CertificateId { get; set; }
        public Certificate? Certificate { get; set; }
        public ICollection<LanguageViewModel>? Languages { get; set; }
      
        public ICollection<GenreViewModel>? Genres { get; set; }
        public ICollection<CastViewModel>? Casts { get; set; }
        public ICollection<CinemaViewModel>? Cinamas { get; set; }
        }

    public class LanguageViewModel
    {
        public int LanguageId { get; set; }
        public string Name { get; set; }
    }

    public class GenreViewModel
    {
        public int GenreId { get; set; }
        public string Name { get; set; }
    }

    public class CastViewModel
        {
        public int CastId { get; set; }
        public string CastName { get; set; }
        public string ImageUrl { get; set; }
        }
   public class CertificateViewModel
        {
        public int CertificateId { get; set; }
        public string Name { get; set; }
        }

    public class HomeMovieViewModel
    {
       public  List<MovieViewModel> Movies { get; set; }
       public  List<LanguageViewModel> Languages { get;set; }
       public List<GenreViewModel> Genres { get;set; }
    }

    public class PaginatedMovieViewModel
    {
        public List<MovieViewModel> Movies { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalItems / PageSize);
    }
}
