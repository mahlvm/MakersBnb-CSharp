namespace MakersBnB.Models;
using System.ComponentModel.DataAnnotations;

public class Space
{
    public string? Name {get; set;}
    public string? Description {get; set;}
    public int? Price {get; set;}
    
    public Space(string name, string description, int price) {
        this.Name = name;
        this.Description = description;
        this.Price = price;
    }
}