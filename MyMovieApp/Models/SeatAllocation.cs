namespace MyMovieApp.Models
    {
    public class SeatAllocation
        {
        public int Id { get; set; }

        public int ShowDetailId { get; set; }

        public ShowDetail ShowDetail { get; set; }

        public string SeatType { get; set; }

        public int NumberOfSeats { get; set; }
        
        public decimal Price { get; set; }
        }
    }
