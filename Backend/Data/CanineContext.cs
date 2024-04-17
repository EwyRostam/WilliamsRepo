using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Data
{
    public class CanineContext : DbContext
    {
        public CanineContext(DbContextOptions<CanineContext> options) : base(options)
        {
            
        }
        public DbSet<Dog> Dogs {get; set;}
    }
}