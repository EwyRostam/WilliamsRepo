using System.ComponentModel.DataAnnotations;

namespace Backend.Models;
public class Dog 
{
    [Key]
    public int Id {get; set;}
    public required string Name {get; set;}
    public int BirthYear {get; set;}
    public DateTime SurrenderAt {get; set;}

}