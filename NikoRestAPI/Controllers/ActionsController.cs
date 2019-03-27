using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Net.ArcanaStudio.NikoSDK;
using Net.ArcanaStudio.NikoSDK.Interfaces;
using Net.ArcanaStudio.NikoSDK.Models;

namespace NikoRestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("MyPolicy")]
    public class ActionsController : ControllerBase
    {
        private readonly NikoClient _nikoClient;

        public ActionsController(INikoService service)
        {
            _nikoClient = service.Client;
            _nikoClient.StartClient();
            _nikoClient.StartEvents();
        }



        // GET: api/Actions
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var actions = await _nikoClient.GetActions();

            if (actions.Data.IsError)
                return  new StatusCodeResult(StatusCodes.Status500InternalServerError);

            return Ok(actions.Data.Actions);
        }

        // GET: api/Actions/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<IActionResult> Get(int id)
        {
            var actions = await _nikoClient.GetActions();

            if (actions.Data.IsError)
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);

            return Ok(actions.Data.Actions.FirstOrDefault(d => d.Id == id));
        }

        // POST: api/Actions
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Action value)
        {
            var response = await _nikoClient.ExecuteCommand(value.Id,value.Value);

            if (response.Data.IsError)
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            _nikoClient.OnValueChanged += NikoClientOnOnValueChanged;
            return Ok(response);
        }

        private void NikoClientOnOnValueChanged(object sender, IEvent e)
        {
         
        }
    }
}
