namespace Backend.Models
{
    public class DogDTO
    {
        public required string Name { get; set; }
        public int BirthYear { get; set; }
        public IFormFile? ImageUrl { get; set; }
    }
}