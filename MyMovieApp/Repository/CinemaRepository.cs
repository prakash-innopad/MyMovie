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
        public Task<CinemaViewModel> Get(int id)
            {
            throw new NotImplementedException();
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
        }
    }
