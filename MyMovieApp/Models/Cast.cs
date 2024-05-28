using System.ComponentModel.DataAnnotations;

namespace MyMovieApp.Models
    {
    public class Cast
        {
        [Key]
        public int Id { get; set; }
        public string CastName { get; set; } = string.Empty;
        public string ImageUrl { get; set; }
        public ICollection<Movie> Movies { get; set; }
        }
    }
