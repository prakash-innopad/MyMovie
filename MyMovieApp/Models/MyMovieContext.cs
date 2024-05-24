using Microsoft.EntityFrameworkCore;

namespace MyMovieApp.Models
{
    public class MyMovieContext : DbContext
    {
        public MyMovieContext()
        {

        }
        public MyMovieContext(DbContextOptions<MyMovieContext> options) : base(options) 
        {
            
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Language> Languages { get; set; }  
        public DbSet<MovieLanguage> MovieLanguages {  get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<MovieGenre> MovieGenres { get; set; }
        public DbSet<Certificate> Certificate { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<MovieLanguage>()
                .HasKey(ml => new { ml.MovieId, ml.LanguageId });

            modelBuilder.Entity<MovieLanguage>()
                .HasOne(ml => ml.Movie)
                .WithMany(m => m.MovieLanguages)
                .HasForeignKey(ml => ml.MovieId);

            modelBuilder.Entity<MovieLanguage>()
            .HasOne(ml => ml.Language)
            .WithMany()
            .HasForeignKey(ml => ml.LanguageId);

            
            modelBuilder.Entity<MovieGenre>()
                .HasKey(ml => new { ml.MovieId, ml.GenreId });

            /*
            modelBuilder.Entity<MovieGenre>()
           .HasOne(mg => mg.Movie)
           .WithMany(m => m.MovieGenres)
           .HasForeignKey(mg => mg.MovieId);

            modelBuilder.Entity<MovieGenre>()
                .HasOne(mg => mg.Genre)
                .WithMany(g => g.MovieGenres)
                .HasForeignKey(mg => mg.GenreId);
          

            modelBuilder.Entity<Movie>()
           .HasOne(m => m.Certificate)
           .WithMany(c => c.Movies)
           .HasForeignKey(m => m.CertificateId);
              */
        }
    }
}
