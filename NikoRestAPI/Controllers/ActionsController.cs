using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Net.ArcanaStudio.NikoSDK;
using Net.ArcanaStudio.NikoSDK.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace NikoRestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("MyPolicy")]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class ActionsController : ControllerBase
    {
        private readonly NikoClient _nikoClient;

        public ActionsController(INikoService service)
        {
            _nikoClient = service.Client;
        }



        // GET: api/Actions
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var actions = await _nikoClient.GetActions();

            if (actions.IsError)
                return  new StatusCodeResult(StatusCodes.Status500InternalServerError);

            return Ok(actions.Data);
        }

        // GET: api/Actions/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<IActionResult> Get(int id)
        {
            var actions = await _nikoClient.GetActions();

            if (actions.IsError)
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);

            return Ok(actions.Data.FirstOrDefault(d => d.Id == id));
        }
        
        // POST: api/Actions
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ExecuteAction value)
        {
            var response = await _nikoClient.ExecuteCommand(value.Id, value.Value);

            if (response.IsError)
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            return Ok(response);
        }
    }
}
