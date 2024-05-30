using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MyMovieApp.ViewModel
    {
    public class MovieUpsertModel : MovieViewModel
        {
        [DisplayName("Poster Image")]
       // [Required(ErrorMessage = "Please upload Movie poster.")]
        public IFormFile PosterImage { get; set; }
        [Required]
        public List<int> SelectedLanguageIds { get; set; }
        [Required]
        public List<int> SelectedGenreIds { get; set;}

        public ICollection<CertificateViewModel>? Certificates { get; set; }
        }
    }
