using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Net.ArcanaStudio.NikoSDK;

namespace NikoRestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationsController : ControllerBase
    {
        private readonly NikoClient _nikoClient;

        public LocationsController(INikoService service)
        {
            _nikoClient = service.Client;
        }


        // GET: api/Locations
        [HttpGet(Name = "GetLocations")]
        public async Task<IActionResult> Get()
        {
            var locationq = await _nikoClient.GetLocations();

            if (locationq.IsError)
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);

            return Ok(locationq.Data);
        }

        // GET: api/Locations/5
        [HttpGet("{id}", Name = "GetLocation")]
        public async Task<IActionResult> Get(int id)
        {
            var locations = await _nikoClient.GetLocations();

            if (locations.IsError)
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);

            return Ok(locations.Data.FirstOrDefault(d => d.Id == id));
        }
    }
}
