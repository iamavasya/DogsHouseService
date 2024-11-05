using Microsoft.AspNetCore.Mvc;

namespace DogsHouseService.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PingController : ControllerBase
    {
        [HttpGet]
        public ActionResult<string> Get()
        {
            return Ok("Dogshouseservice.Version1.0.1");
        }
    }
}
