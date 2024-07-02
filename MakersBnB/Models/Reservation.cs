using System;
using System.ComponentModel.DataAnnotations;

namespace MakersBnB.Models
{
    public class Reservation
    {
        [Key]
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public int SpaceId { get; set; } // Foreign key for Space
        public Space Space { get; set; } = null!; // Navigation property to Space

        public int UserId { get; set; } // Foreign key for User
        public User User { get; set; } = null!; // Navigation property to User
    }
}
