using MyMovieApp.Models;
using System.ComponentModel;

namespace MyMovieApp.ViewModel
    {
    public class CinemaViewModel
        {
        public int CinemaId { get; set; }
        public string Name { get; set; }
        public int AddressId { get; set; }
        [DisplayName("Detailed Address")]
        public string DetaildedAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        [DisplayName("Postal Code")]
        public string? PostalCode { get; set; }

        public ICollection<ShowDetailModel>? ShowDetails { get; set; }
        }
    public class ShowDetailModel
        {
        public DateTime ShowTime { get; set; }
        public int ScreenNumber { get; set; }

        public ICollection<SeatAllocationModel> SeatAllocations { get; set; }
        }
    public class SeatAllocationModel
        {
        public int Id { get; set; }

        public int ShowDetailId { get; set; }

        public ShowDetail ShowDetail { get; set; }

        public string SeatType { get; set; }

        public int NumberOfSeats { get; set; }

        public decimal Price { get; set; }
        }
    }
