namespace HotelReservationSystem.Models
{
    public class Reservation
    {
        public string Name { get; set; }
        public string ContactNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public int NumberOfPeople { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        
    }
    public class ReservationModel
    {
        public string Name { get; set; }
        public string ContactNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public int NumberOfPeople { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
    }
}
