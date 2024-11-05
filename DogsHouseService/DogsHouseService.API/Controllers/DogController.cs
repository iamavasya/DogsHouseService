using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DogsHouseService.Infrastructure.Models.DTOs;
using DogsHouseService.Infrastructure.Models.Entities;
using DogsHouseService.BusinessLogic.Interfaces;

namespace DogsHouseService.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DogController : ControllerBase
    {
        private readonly IDogsService _dogsService;
        public DogController(IDogsService dogsService)
        {
            _dogsService = dogsService;
        }
        [HttpGet]
        public async Task<ActionResult<Dog>> Get(int id)
        {
            var dog = await _dogsService.GetByIdAsync(id);
            return Ok(dog);
        }
        [HttpPost]
        public async Task<ActionResult<Dog?>> Post([FromBody] DogDto dog)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingDog = await _dogsService.GetDogByNameAsync(dog.Name);
            if (existingDog != null)
            {
                return Conflict($"A dog with the name '{dog.Name}' already exists.");
            }

            var createdDog = await _dogsService.CreateDogAsync(dog);
            if (createdDog != null)
            {
                return CreatedAtAction(nameof(Get), new { id = createdDog.Id }, createdDog);
            }

            return BadRequest("Failed to create dog");
        }
    }
}
