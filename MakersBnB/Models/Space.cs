using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MakersBnB.Models
{
    public class Space
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "The Name field is required.")]
        public string? Name { get; set; }

        public string? Description { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Price must be a positive number.")]
        public int? Price { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Bedrooms must be a positive number.")]
        public int Bedrooms { get; set; }

        public string? Rules { get; set; }

        public string? PhotoPath { get; set; }

        public int UserId { get; set; } // Foreign key for User
        public User User { get; set; } = null!; // Navigation property to User

        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>(); // Collection of reservations


        public Space(string name, string description, int price, int bedrooms, string rules)
        {
            Name = name;
            Description = description;
            Price = price;
            Bedrooms = bedrooms;
            Rules = rules;
        }

    
        public Space() { }
    }
}
