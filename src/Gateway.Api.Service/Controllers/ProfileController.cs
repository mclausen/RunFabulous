using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Profile.Messages.External.Commands;
using Rebus.Bus;

namespace Gateway.Api.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IBus bus;

        public ProfileController(IBus bus)
        {
            this.bus = bus;
        }

        [HttpPost]
        public async Task<ActionResult> CreateProfile([FromBody]ProfileDto profileDto)
        {

            await bus.Send(new CreateProfileCommand(profileDto.Name));
            return Ok();
        }
    }
}
