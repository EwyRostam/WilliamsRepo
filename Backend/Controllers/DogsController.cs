
using Backend.Data;
using Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class DogsController : ControllerBase
    {
        private readonly CanineContext _context;

        public DogsController(CanineContext context)
        {
            _context = context;
        }

        [HttpGet]

         public async Task<ActionResult<IEnumerable<Dog>>> GetAllDogs()
        {
            var result = await _context.Dogs.ToListAsync();
            if (result == null){
                return NotFound();
            }
            else{
                return result;
            }
        }

        
        [HttpGet("{id}")]
        public async Task<ActionResult<Dog>> GetDog(int id)
        {
            var dog = await _context.Dogs.FirstOrDefaultAsync(dog => dog.Id == id);
            return dog == null ? BadRequest() : dog;
        }

        [HttpPost]
        public async Task<ActionResult<Dog>> PostDog(Dog dog)
        {
            var newDog = new Dog(){
                Id = dog.Id,
                Name = dog.Name,
                BirthYear = dog.BirthYear,
                SurrenderAt = DateTime.Now,
            };
            
            await _context.Dogs.AddAsync(newDog);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetDog), new{id = newDog.Id}, newDog);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteDog(int id)
        {
            var dog = await _context.Dogs.FirstOrDefaultAsync(dog => dog.Id == id);
            if (dog != null)
            {
                _context.Dogs.Remove(dog);
            }
            return NoContent();
        }
    }
}