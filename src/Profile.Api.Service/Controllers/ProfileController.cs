using Microsoft.AspNetCore.Mvc;

namespace Profile.Api.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        [HttpGet]
        public ActionResult<Domain.Profile> Get(string id)
        {
            return Ok(null);
        }
    }
}
