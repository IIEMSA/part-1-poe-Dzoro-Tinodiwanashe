// Event.cs
using System;
using System.ComponentModel.DataAnnotations;

namespace CloudPOEpart1.Models
{
    public class Event
    {
        public int EventID { get; set; }

        [Required]
        public string EventName { get; set; }

        public string? Description { get; set; }
        public DateTime? EventDate { get; set; }
        public string? ImageUrl { get; set; }

        // Foreign key for Venue
        public int VenueID { get; set; }
        public Venue Venue { get; set; } // Navigation property

        // Other properties related to the event
    }
}
