using Microsoft.EntityFrameworkCore;
using MyMovieApp.Interface;
using MyMovieApp.Models;
using MyMovieApp.ViewModel;

namespace MyMovieApp.Repository
    {
    public class CinemaRepository : ICinemaRepository
        {
        private readonly MyMovieContext _dbContext;
        public CinemaRepository(MyMovieContext dbContext)
            {
            _dbContext = dbContext;
            }


        public async Task<List<CinemaViewModel>> GetAll()
            {
            var Cinemas = await _dbContext.Cinemas.Include(c => c.Address).ToListAsync();
            return Cinemas.Select(c => new CinemaViewModel
                {
                CinemaId = c.CinemaId,
                Name = c.Name,
                City = c.Address.City,
                State = c.Address.State,
                Country = c.Address.Country,
                PostalCode = c.Address.PostalCode,
                DetaildedAddress = c.Address.DetaildedAddress,
                }).ToList();

            }
        public async Task<CinemaViewModel> Get(int id)
            {
            var cinema = await _dbContext.Cinemas.Include(c => c.Address).FirstOrDefaultAsync(c => c.CinemaId == id);

            if (cinema != null)
                {
                return new CinemaViewModel
                    {
                    CinemaId = cinema.CinemaId,
                    Name = cinema.Name,
                    City = cinema.Address.City,
                    State = cinema.Address.State,
                    Country = cinema.Address.Country,
                    DetaildedAddress = cinema.Address.DetaildedAddress,
                    AddressId = cinema.Address.AddressId,
                    PostalCode = cinema.Address?.PostalCode,
                    };
                }
            else
                {
                return null;
                }
                    
        }


    public async Task<object> AddCinema(CinemaViewModel viewModel)
        {
        try
            {
            var cinema = new Cinema
                {
                Name = viewModel.Name,
                Address = new Address
                    {
                    AddressId = viewModel.AddressId,
                    DetaildedAddress = viewModel.DetaildedAddress,
                    City = viewModel.City,
                    State = viewModel.State,
                    Country = viewModel.Country,
                    PostalCode = viewModel.PostalCode
                    }
                };
            _dbContext.Cinemas.Add(cinema);
            await _dbContext.SaveChangesAsync();
            return viewModel;
            }
        catch (Exception ex)
            {
            throw new NotImplementedException();
            }
        }

        public async Task<bool> UpdateCinema(CinemaViewModel cinemaViewModel)
            {
              var cinema = await _dbContext.Cinemas.Include(c => c.Address).FirstOrDefaultAsync(c => c.CinemaId == cinemaViewModel.CinemaId);
              if (cinema != null)
                {
                cinema.Name = cinemaViewModel.Name;
                cinema.Address.City = cinemaViewModel.City;
                cinema.Address.State = cinemaViewModel.State;
                cinema.Address.Country = cinemaViewModel.Country;
                cinema.Address.DetaildedAddress = cinemaViewModel.DetaildedAddress;
                cinema.Address.PostalCode  = cinemaViewModel.PostalCode;
                await _dbContext.SaveChangesAsync();
                return true;
                }
            else
                {
                    return false;
                }
            }

        #region Delete Cinema
        public async  Task<bool> DeleteCinema(int id)
            {
              var cinema= _dbContext.Cinemas.Find(id);
            if (cinema != null)
                {
                _dbContext.Remove(cinema);
               await _dbContext.SaveChangesAsync();
                return true;
                }
            else
                {
                return false;
                }

            }
    #endregion
        }
    }
