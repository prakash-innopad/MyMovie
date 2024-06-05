using MyMovieApp.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MyMovieApp.ViewModel
    {
    public class CinemaViewModel
        {
        public int CinemaId { get; set; }
        [Required(ErrorMessage ="Please Enter Cinema Name.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please Enter Cinema Name.")]
        public int AddressId { get; set; }
        [Required(ErrorMessage = "Please Enter Detailed Address.")]
        [DisplayName("Detailed Address")]
        public string DetaildedAddress { get; set; }
        [Required(ErrorMessage = "Please Enter City Name.")]
        public string City { get; set; }
        [Required(ErrorMessage = "Please Enter State Name.")]
        public string State { get; set; }
        [Required(ErrorMessage = "Please Enter Country Name.")]
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
