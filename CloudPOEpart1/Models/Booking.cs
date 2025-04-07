namespace CloudPOEpart1.Models
{
    public class Booking
    {
        public int BookingID { get; set; }
        public int EventID { get; set; }
        public int VenueID { get; set; }
        public DateTime BookingDate { get; set; }
        public Event Event { get; set; }
        public Venue Venue { get; set; }

        // Add constructor to initialize non-nullable properties
        public Booking()
        {
            this.BookingDate = DateTime.Now;
        }
    }
}