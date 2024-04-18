
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
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
        private readonly string _blobConnectionString;
        private readonly BlobServiceClient _blobServiceClient;
        private readonly BlobContainerClient _containerClient;

        public DogsController(CanineContext context, string blobConnectionString, BlobServiceClient blobServiceClient, BlobContainerClient containerClient)
        {
            _context = context;
            _blobConnectionString = blobConnectionString;
            _blobServiceClient = blobServiceClient;
            _containerClient = containerClient;
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
        public async Task<ActionResult<Dog>> PostDog([FromForm]DogDTO dog)
        {
            string fileName = dog.Name + "_" + Guid.NewGuid().ToString() + ".gif";

            var blobClient = _containerClient.GetBlobClient(fileName);

            using (var stream = dog.ImageUrl!.OpenReadStream())
            {
                await blobClient.UploadAsync(stream, new BlobHttpHeaders {ContentType = dog.ImageUrl.ContentType}, conditions: null);
            }

            var imageResponseUrl = blobClient.Uri.ToString();

            var newDog = new Dog(){
                Name = dog.Name,
                BirthYear = dog.BirthYear,
                SurrenderAt = DateTime.Now,
                ImageUrl = imageResponseUrl
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