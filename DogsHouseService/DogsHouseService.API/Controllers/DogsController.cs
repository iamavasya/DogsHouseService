using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DogsHouseService.Infrastructure.Models.Entities;
using DogsHouseService.BusinessLogic.Interfaces;

namespace DogsHouseService.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DogsController : ControllerBase
    {
        private readonly IDogsService _dogsService;
        public DogsController(IDogsService dogsService)
        {
            _dogsService = dogsService;
        }

        [HttpGet]
        public async Task<ActionResult<IList<Dog>>> Get(string attribute = "name", string order = "asc", int pageNumber = 1, int pageSize = 10)
        {
            var dogs = await _dogsService.GetDogsAsync(pageNumber, pageSize, attribute, order);
            return Ok(dogs);
        }
    }
}
