namespace MakersBnB.Models;
using System.ComponentModel.DataAnnotations;

public class Space
{
    [Key]
    public int Id {get; set;}
    public string? Name {get; set;}
    public string? Description {get; set;}
    public int? Price {get; set;}
    public int Bedrooms { get; set; }
    public string Rules{get; set;}

    public int UserId { get; set; } // Foreign key for User
    public User User { get; set; } = null!; // Navigation property to User

    public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>(); // Collection of reservations


    public Space(string name, string description, int price, int bedrooms, string rules) {
        this.Name = name;
        this.Description = description;
        this.Price = price;
        this.Bedrooms = bedrooms;
        this.Rules = rules;

    }
    public Space() {}
}