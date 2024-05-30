using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MyMovieApp.Interface;
using MyMovieApp.Migrations;
using MyMovieApp.Models;
using MyMovieApp.ViewModel;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static MyMovieApp.Classes.EnumManger;

namespace MyMovieApp.Repository
    {
    public class MovieRepository : IMovieRepository
        {
        private readonly MyMovieContext _dbContext;
        public MovieRepository(MyMovieContext dbContext)
            {
            _dbContext = dbContext;
            }
        public async Task<object> AddMovie(MovieUpsertModel movieViewModel)
            {
            try
                {
                //  var Languages = _dbContext.Languages.Where(l => movieViewModel.SelectedLanguageIds.Contains(l.LanguageId)).ToList();

                var movie = new Movie()
                    {
                   // ImageLink = movieViewModel.ImageLink,
                    Title = movieViewModel.Title,
                    Price = movieViewModel.Price,
                    ReleaseDate = movieViewModel.ReleaseDate,
                    Synopsis = movieViewModel.Synopsis,
                    TrailerLink = movieViewModel.TrailerLink,
                    Runtime = movieViewModel.Runtime,
                    MovieLanguages = movieViewModel.SelectedLanguageIds.Select(id => new MovieLanguage { LanguageId = id }).ToList(),
                    MovieGenres = movieViewModel.SelectedGenreIds.Select(id => new MovieGenre { GenreId = id }).ToList()
                    };

                string fileName = string.Empty;
                if (movieViewModel.PosterImage != null)
                    {
                      fileName = Guid.NewGuid().ToString() + Path.GetExtension(movieViewModel.PosterImage.FileName);
                      movie.ImageLink = fileName;
                    }
                await _dbContext.Movies.AddAsync(movie);              
                _dbContext.SaveChanges();
                if (movieViewModel.PosterImage != null)
                    {
                    HandleImage(movieViewModel, fileName);
                    }
                return movieViewModel;
                }
            catch (Exception ex)
                {

                return null;
                }
            }

       bool HandleImage(MovieUpsertModel movieViewModel, string fileName)
            {
            var filePath = Path.Combine("wwwroot", "img", fileName);
            var filePathDirectory = Path.Combine(Directory.GetCurrentDirectory(), filePath);
            using (var fileStream = new FileStream(filePathDirectory, FileMode.Create))
                {
                movieViewModel.PosterImage.CopyTo(fileStream);
                }
            return true;
            }

        public async Task<bool> DeleteMovie(int id)
            {
            var movie = _dbContext.Movies.Find(id);
            if (movie != null)
                {
                _dbContext.Movies.Remove(movie);
                await _dbContext.SaveChangesAsync();
                return true;
                }
            else
                {
                return false;
                }
            }


        public async Task<MovieViewModel> Get(int id, string city)
            {
            var result = await _dbContext.Movies.Where(x => x.Id == id)
                  .Include(m => m.MovieLanguages)
                       .ThenInclude(ml => ml.Language)
                   .Include(m => m.MovieGenres)
                       .ThenInclude(mg => mg.Genre)
                   .Include(m => m.Certificate)
                   .Include(m => m.Casts)
                       .FirstOrDefaultAsync();

            var cinemas = await _dbContext.MovieCinemas.Where(mc => mc.MovieId == id && mc.Cinema.Address.City == city)
                .Include(m => m.Cinema)
                  .ThenInclude(m => m.Address)
                .Include(m => m.ShowDetails)
                 .ThenInclude(s => s.SeatAllocations)
                .ToListAsync();

            //var res = _dbContext.Movies.FromSqlRaw("EXEC SP_GetMoviesById @MovieId", new SqlParameter("@MovieId", id)).AsEnumerable();
            if (result != null)
                {
                return new MovieViewModel()
                    {
                    Id = id,
                    ImageLink = result.ImageLink,
                    Title = result.Title,
                    Price = result.Price,
                    ReleaseDate = result?.ReleaseDate,
                    Runtime = result.Runtime,
                    Format = result.Format,
                    Likes = result.Likes,
                    DisLikes = result.DisLikes,
                    Synopsis = result.Synopsis,
                    TrailerLink = result.TrailerLink,
                    Genres = result.MovieGenres.Select(ml => new GenreViewModel
                        {
                        GenreId = ml.Genre.GenreId,
                        Name = ml.Genre.Name
                        }).ToList(),
                    Languages = result.MovieLanguages.Select(ml => new LanguageViewModel
                        {
                        LanguageId = ml.Language.LanguageId,
                        Name = ml.Language.Name
                        }).ToList(),
                    Casts = result.Casts.Select(ml => new CastViewModel
                        {
                        CastId = ml.Id,
                        CastName = ml.CastName,
                        ImageUrl = ml.ImageUrl
                        }).ToList(),

                    Certificate = result.Certificate,
                    Cinamas = cinemas.Select(c => new CinemaViewModel
                        {
                        CinemaId = c.CinemaId,
                        Name = c.Cinema.Name,
                        City = c.Cinema.Address.City,
                        State = c.Cinema.Address.State,
                        Country = c.Cinema.Address.Country,
                        DetaildedAddress = c.Cinema.Address.DetaildedAddress,
                        AddressId = c.Cinema.Address.AddressId,
                        PostalCode = c.Cinema.Address?.PostalCode,
                        ShowDetails = c.ShowDetails?.Select(m => new ShowDetailModel
                            {
                            ScreenNumber = m.ScreenNumber,
                            ShowTime = m.ShowTime,
                            SeatAllocations = m.SeatAllocations.Select(s => new SeatAllocationModel
                                {
                                Id = s.Id,
                                SeatType = s.SeatType,
                                NumberOfSeats = s.NumberOfSeats,
                                Price = s.Price
                                }).ToList()
                            }).ToList()
                        }).ToList()
                    };
                }
            else
                {
                return null;
                }

            }

        public async Task<List<MovieViewModel>> GetAll()
            {
            /*
            var movies = await _dbContext.Movies.ToListAsync();
            return movies.Select(movie => new MovieViewModel
            {
                Id = movie.Id,
                ImageLink = movie.ImageLink,
                ReleaseDate = movie.ReleaseDate,
                Price = movie.Price,
                Title = movie.Title,
            }
             ).ToList();

            */

            var movies = await _dbContext.Movies
                  .Include(m => m.MovieLanguages)
                      .ThenInclude(ml => ml.Language)
                  .Include(m => m.MovieGenres)
                      .ThenInclude(mg => mg.Genre)
                  .ToListAsync();


            var movieViewModels = movies.Select(m => new MovieViewModel
                {
                Id = m.Id,
                Title = m.Title,
                ImageLink = m.ImageLink,
                ReleaseDate = m.ReleaseDate,
                Price = m.Price,
                Languages = m.MovieLanguages.Select(ml => new LanguageViewModel
                    {
                    LanguageId = ml.Language.LanguageId,
                    Name = ml.Language.Name
                    }).ToList(),
                Genres = m.MovieGenres.Select(ml => new GenreViewModel
                    {
                    GenreId = ml.Genre.GenreId,
                    Name = ml.Genre.Name
                    }).ToList()
                }).ToList();
            return movieViewModels;

            }

        public async Task<List<MovieViewModel>> FetchFilteredMovies(List<int> genreIds, string format, int pageNumber, int PageSize, int languageId, int sort, bool topSeller)
            {
            try
                {
                var query = _dbContext.Movies
                      .Include(m => m.MovieLanguages)
                          .ThenInclude(ml => ml.Language)
                      .Include(m => m.MovieGenres)
                          .ThenInclude(mg => mg.Genre)
                      .Include(m => m.Certificate)
                      .AsQueryable();
                if (languageId != 0)
                    {
                    query = query.Where(m => m.MovieLanguages.Any(ml => ml.LanguageId == languageId));
                    }
                if (genreIds != null && genreIds.Any())
                    {
                    query = query.Where(m => m.MovieGenres.Any(mg => genreIds.Contains(mg.GenreId)));
                    }
                if (!string.IsNullOrEmpty(format))
                    {
                    query = query.Where(m => m.Format == format);
                    }

                switch (sort)
                    {
                    case 1:
                        query = query.OrderByDescending(m => m.ReleaseDate);
                        break;
                    case 2:
                        query = query.OrderBy(m => m.ReleaseDate);
                        break;
                    default:
                        break;
                    }

                var totalMovies = await query.CountAsync();
                var movies = await query.Skip((pageNumber - 1) * PageSize)
                    .Take(PageSize)
                    .ToListAsync();

                return movies.Select(m => new MovieViewModel
                    {
                    Id = m.Id,
                    Title = m.Title,
                    ImageLink = m.ImageLink,
                    ReleaseDate = m.ReleaseDate,
                    Price = m.Price,
                    Languages = m.MovieLanguages.Select(ml => new LanguageViewModel
                        {
                        LanguageId = ml.LanguageId,
                        Name = ml.Language.Name
                        }).ToList(),
                    Genres = m.MovieGenres.Select(gr => new GenreViewModel
                        {
                        GenreId = gr.GenreId,
                        Name = gr.Genre.Name
                        }).ToList()
                    }).ToList();
                }
            catch (Exception ex)
                {
                return null;
                }

            }
        public async Task<HomeMovieViewModel> GetMoviesWithFilterytypes()
            {
            var homeMovieViewModel = new HomeMovieViewModel();
            var movies = await _dbContext.Movies
                  .Include(m => m.MovieLanguages)
                      .ThenInclude(ml => ml.Language)
                  .ToListAsync();

            homeMovieViewModel.Movies = movies.Select(movie => new MovieViewModel
                {
                Id = movie.Id,
                ImageLink = movie.ImageLink,
                ReleaseDate = movie.ReleaseDate,
                Price = movie.Price,
                Title = movie.Title,
                Languages = movie.MovieLanguages.Select(ml => new LanguageViewModel
                    {
                    LanguageId = ml.Language.LanguageId,
                    Name = ml.Language.Name
                    }).ToList()
                }
             ).ToList();
            homeMovieViewModel.Languages = _dbContext.Languages.Select(language => new LanguageViewModel
                {
                LanguageId = language.LanguageId,
                Name = language.Name,
                }).ToList();
            homeMovieViewModel.Genres = _dbContext.Genres.Select(language => new GenreViewModel
                {
                GenreId = language.GenreId,
                Name = language.Name,
                }).ToList();
            return homeMovieViewModel;
            }

        public async Task<object> UpdateMovie(MovieViewModel movieViewModel)
            {
            var movie = _dbContext.Movies.FirstOrDefault(x => x.Id == movieViewModel.Id);
            if (movie != null)
                {
                movie.Title = movieViewModel.Title;
                movie.ImageLink = movieViewModel.ImageLink;
                movie.ReleaseDate = movieViewModel.ReleaseDate;
                movie.Price = movieViewModel.Price;
                await _dbContext.SaveChangesAsync();
                return true;
                }
            else
                {
                return false;
                }
            }

        public async Task<List<LanguageViewModel>> GetAllLanguages()
            {

            var languages = _dbContext.Languages.Select(language => new LanguageViewModel
                {
                LanguageId = language.LanguageId,
                Name = language.Name,
                }).ToList();
            return languages;
            /*
            var Languages = _dbContext.Languages
                        .Select(language => new SelectListItem
                            {
                            Value = language.LanguageId.ToString(),
                            Text = language.Name,
                            }).ToList();
            return Languages;
            }
            */
            }

        public async Task<List<GenreViewModel>> GetAllGenres()
            {

            var genres = _dbContext.Genres.Select(genre => new GenreViewModel
                {
                GenreId = genre.GenreId,
                Name = genre.Name,
                }).ToList();
            return genres;           
            }   

        public async Task<MovieUpsertModel> GetUpsertModel()
            {
            var model = new MovieUpsertModel();
            try
                {
                model.Languages = _dbContext.Languages.Select(language => new LanguageViewModel
                    {
                    LanguageId = language.LanguageId,
                    Name = language.Name,
                    }).ToList();
                model.Genres = _dbContext.Genres.Select(genre => new GenreViewModel
                    {
                    GenreId = genre.GenreId,
                    Name = genre.Name,
                    }).ToList();
                model.Certificates = _dbContext.Certificate.Select(cer => new CertificateViewModel
                    {
                    CertificateId = cer.CertificateId,
                    Name = cer.Name,
                    }).ToList();
                return model;
                }
            catch
                {
                  return model;
                }
            }
        }
    }


 
