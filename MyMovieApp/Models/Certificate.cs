namespace MyMovieApp.Models
{
    public class Certificate
    {
        public int CertificateId {  get; set; }
        public string Name { get; set; }

        public ICollection<Movie> Movies { get; set;}
    }
}
