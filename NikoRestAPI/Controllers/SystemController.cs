using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Net.ArcanaStudio.NikoSDK;
namespace NikoRestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SystemController : ControllerBase
    {
        private readonly NikoClient _nikoClient;

        public SystemController(INikoService service)
        {
            _nikoClient = service.Client;
            _nikoClient.StartClient();
            _nikoClient.StartEvents();
        }

        // GET: api/System
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var si = await _nikoClient.GetSystemInfo();

            if (si.Data.IsError)
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);

            return Ok(si.Data);
        }
    }
}
