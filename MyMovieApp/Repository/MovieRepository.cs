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

        #region Add Movie
        public async Task<object?> AddMovie(MovieUpsertModel movieViewModel)
            {
            try
                {
                //  var Languages = _dbContext.Languages.Where(l => movieViewModel.SelectedLanguageIds.Contains(l.LanguageId)).ToList();

                var movie = new Movie()
                    {
                    // ImageLink = movieViewModel.ImageLink,
                    Title = movieViewModel.Title,
                    ReleaseDate = movieViewModel.ReleaseDate,
                    Synopsis = movieViewModel.Synopsis,
                    TrailerLink = movieViewModel.TrailerLink,
                    Runtime = movieViewModel.Runtime,
                    CertificateId = movieViewModel.CertificateId,
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
        #endregion

        #region Delete Movie
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
        #endregion

        #region Get Movie by Id
        public async Task<MovieViewModel> Get(int id, string city, bool isForEdit = false)
            {
            var result = await _dbContext.Movies.Where(x => x.Id == id)
                  .Include(m => m.MovieLanguages)
                       .ThenInclude(ml => ml.Language)
                   .Include(m => m.MovieGenres)
                       .ThenInclude(mg => mg.Genre)
                   .Include(m => m.Certificate)
                   .Include(m => m.Casts)
                       .FirstOrDefaultAsync();

            //var res = _dbContext.Movies.FromSqlRaw("EXEC SP_GetMoviesById @MovieId", new SqlParameter("@MovieId", id)).AsEnumerable();
            if (result != null)
                {
                var viewModel = new MovieViewModel()
                    {
                    Id = id,
                    ImageLink = result.ImageLink,
                    Title = result.Title,
                    ReleaseDate = result?.ReleaseDate,
                    Runtime = result.Runtime,
                    Format = result.Format,
                    Likes = result.Likes,
                    DisLikes = result.DisLikes,
                    Synopsis = result.Synopsis,
                    TrailerLink = result.TrailerLink,
                    Certificate = result.Certificate,
                    
                    };

                if (isForEdit == true)
                    {
                    var allGenres = _dbContext.Genres.ToList();
                    var selectedGenreIds = result.MovieGenres.Select(g => g.GenreId).ToList();
                    var allGenreViewModels = allGenres.Select(g => new GenreViewModel
                        {
                        GenreId = g.GenreId,
                        Name = g.Name,
                        IsSelected = selectedGenreIds.Contains(g.GenreId)
                        }).ToList();
                    viewModel.Genres = allGenreViewModels;

                    var allLanguages = _dbContext.Languages.ToList();
                    var selectedLanguagesIds = result.MovieLanguages.Select(g => g.LanguageId).ToList();
                    var allLanguageViewModels = allLanguages.Select(g => new LanguageViewModel
                        {
                        LanguageId = g.LanguageId,
                        Name = g.Name,
                        IsSelected = selectedLanguagesIds.Contains(g.LanguageId)
                        }).ToList();
                    viewModel.Languages = allLanguageViewModels;
                    }
                else
                    {
                    var cinemas = await _dbContext.MovieCinemas.Where(mc => mc.MovieId == id && mc.Cinema.Address.City == city)
                .Include(m => m.Cinema)
                  .ThenInclude(m => m.Address)
                .Include(m => m.ShowDetails)
                 .ThenInclude(s => s.SeatAllocations)
                .ToListAsync();

                    viewModel.Languages = result.MovieLanguages.Select(ml => new LanguageViewModel
                        {
                        LanguageId = ml.Language.LanguageId,
                        Name = ml.Language.Name
                        }).ToList();
                    viewModel.Genres = result.MovieGenres?.Select(ml => new GenreViewModel
                        {
                        GenreId = ml.Genre.GenreId,
                        Name = ml.Genre.Name
                        }).ToList();
                    viewModel.Casts = result.Casts?.Select(ml => new CastViewModel
                        {
                        CastId = ml.Id,
                        CastName = ml.CastName,
                        ImageUrl = ml.ImageUrl
                        }).ToList();
                    viewModel.Cinamas = cinemas.Select(c => new CinemaViewModel
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
                        }).ToList();
                    }
                return viewModel;
                }
            else
                {
                return null;
                }

            }
        #endregion

        #region Get All Movies
        public async Task<List<MovieViewModel>> GetAll()
            {
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
                Synopsis = m.Synopsis,
                Runtime = m.Runtime,
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
        #endregion

        #region FetchFilteredMovies
        public async Task<List<MovieViewModel>> FetchFilteredMovies(List<int> genreIds, string format, string searchText, int pageNumber, int PageSize, int languageId, int sort, bool topSeller)
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
                if (!string.IsNullOrEmpty(searchText))
                    {
                    query = query.Where(m => m.Title.Contains(searchText));
                    }
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
        #endregion

        #region Get Movies for Home page 
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
        #endregion

        #region Update Movie
        public async Task<object> UpdateMovie(MovieUpsertModel movieViewModel)
            {
            var movie = _dbContext.Movies
                .Include(m => m.MovieLanguages)
                .Include(m => m.MovieGenres)
                 .FirstOrDefault(x => x.Id == movieViewModel.Id);
            if (movie != null)
                {
                movie.Title = movieViewModel.Title;
                //movie.ImageLink = movieViewModel.ImageLink;
                movie.ReleaseDate = movieViewModel.ReleaseDate;
                movie.TrailerLink = movieViewModel.TrailerLink;
                movie.Runtime = movieViewModel.Runtime;
                movie.Synopsis = movieViewModel.Synopsis;
                movie.CertificateId = movieViewModel.CertificateId;
                //  movie.MovieLanguages = movieViewModel.SelectedLanguageIds.Select(id => new MovieLanguage { LanguageId = id }).ToList();

                var existingLanguageIds = movie.MovieLanguages.Select(ml => ml.LanguageId).ToList();
                var existingGenreIds = movie.MovieGenres.Select(ml => ml.GenreId).ToList();

                movieViewModel.SelectedLanguageIds.Except(existingLanguageIds).ToList().ForEach(l => movie.MovieLanguages.Add(new MovieLanguage { MovieId = movie.Id, LanguageId = l })); ;
                var genreIdsToAdd = movieViewModel.SelectedGenreIds.Except(existingGenreIds).ToList();

                foreach (var genreId in genreIdsToAdd)
                    {
                    movie.MovieGenres.Add(new MovieGenre { MovieId = movie.Id, GenreId = genreId });
                    }

                var languageIdsToRemove = existingLanguageIds.Except(movieViewModel.SelectedLanguageIds).ToList();
                var genreIdsToRemove = existingGenreIds.Except(movieViewModel.SelectedGenreIds).ToList();
                foreach (var languageId in languageIdsToRemove)
                    {
                    var movieLanguage = movie.MovieLanguages.FirstOrDefault(ml => ml.LanguageId == languageId);
                    if (movieLanguage != null)
                        {
                        _dbContext.MovieLanguages.Remove(movieLanguage);
                        }
                    }
                foreach (var genreId in genreIdsToRemove)
                    {
                    var movieGenre = movie.MovieGenres.FirstOrDefault(ml => ml.GenreId == genreId);
                    if (movieGenre != null)
                        {
                        _dbContext.MovieGenres.Remove(movieGenre);
                        }
                    }


                if (movieViewModel.PosterImage != null)
                    {
                    string fileName = string.Empty;
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img");
                    fileName = Guid.NewGuid().ToString() + Path.GetExtension(movieViewModel.PosterImage.FileName);
                    if (!string.IsNullOrEmpty(movieViewModel.ImageLink))
                        {
                        var oldFilePath = Path.Combine(filePath, movieViewModel.ImageLink);
                        if (System.IO.File.Exists(oldFilePath))
                            {
                            System.IO.File.Delete(oldFilePath);
                            }
                        }

                    using (var fileStream = new FileStream(Path.Combine(filePath, fileName), FileMode.Create))
                        {
                        movieViewModel.PosterImage.CopyTo(fileStream);
                        }
                    movie.ImageLink = fileName;
                    }
                await _dbContext.SaveChangesAsync();
                return true;
                }
            else
                {
                return false;
                }
            }
        #endregion

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



