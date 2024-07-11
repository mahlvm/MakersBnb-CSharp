namespace MakersBnB.Models;
using System.ComponentModel.DataAnnotations;

public class User
{
    [Key]
    public int Id {get; set;}
    public string Username {get; set;}
    public string Email {get; set;}
    public string Password {get; set;}

    public ICollection<Space> Spaces { get; } = new List<Space>(); 
    public ICollection<Reservation> Reservations { get; } = new List<Reservation>(); 


    public User(string username, string email, string password) {
        this.Username = username;
        this.Email = email;
        this.Password = password;


    }
    public User() {}
}
