using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Identity.Service.Models;
using Identity.Service.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {

        private readonly IIdentityService _identityService;

        public IdentityController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        [HttpPost("sign-up/player")]
        public async Task<IActionResult> SignUpPlayer([FromBody]SignUpPlayerModel model)
        {
            await _identityService.SignUpPlayer(model);

            return NoContent();
        }

        [HttpPost("sign-up/coach")]
        public async Task<IActionResult> SignUpCoach([FromBody]SignUpCoachModel model)
        {
            await _identityService.SignUpCoach(model);

            return NoContent();
        }

        [HttpPost("sign-in")]
        public async Task<IActionResult> SignIn([FromBody]SignInModel model)
            => Ok(await _identityService.SignIn(model));

        
    }
}