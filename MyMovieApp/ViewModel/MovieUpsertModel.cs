using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MyMovieApp.ViewModel
    {
    public class MovieUpsertModel : MovieViewModel 
        {
        public MovieUpsertModel() { }
        public MovieUpsertModel(MovieViewModel movieViewModel) {
            Id = movieViewModel.Id;
            Title = movieViewModel.Title;
            ImageLink = movieViewModel.ImageLink;
            Title = movieViewModel.Title;
            Price = movieViewModel.Price;
            ReleaseDate = movieViewModel?.ReleaseDate;
            Runtime = movieViewModel.Runtime;
            Format = movieViewModel.Format;
            Likes = movieViewModel.Likes;
            DisLikes = movieViewModel.DisLikes;
            Synopsis = movieViewModel.Synopsis;
            TrailerLink = movieViewModel.TrailerLink;
            Genres = movieViewModel.Genres;
            Languages = movieViewModel.Languages;
            Casts = movieViewModel.Casts;
            Certificate = movieViewModel.Certificate;
            }

        [DisplayName("Poster Image")]
       // [Required(ErrorMessage = "Please upload Movie poster.")]
        public IFormFile? PosterImage { get; set; }
        [Required(ErrorMessage ="Please select at lease one Language")]
        public List<int> SelectedLanguageIds { get; set; }
       // [Required]
        public List<int>? SelectedGenreIds { get; set;}

        public ICollection<CertificateViewModel>? Certificates { get; set; }
        }
    }
