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
            _nikoClient.StartClient();
            _nikoClient.StartEvents();
        }


        // GET: api/Locations
        [HttpGet(Name = "GetLocations")]
        public async Task<IActionResult> Get()
        {
            var locationq = await _nikoClient.GetLocations();

            if (locationq.Data.IsError)
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);

            return Ok(locationq.Data.Locations);
        }

        // GET: api/Locations/5
        [HttpGet("{id}", Name = "GetLocation")]
        public async Task<IActionResult> Get(int id)
        {
            var locations = await _nikoClient.GetLocations();

            if (locations.Data.IsError)
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);

            return Ok(locations.Data.Locations.FirstOrDefault(d => d.Id == id));
        }
    }
}
