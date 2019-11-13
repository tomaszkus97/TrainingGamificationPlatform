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

        [HttpPost("sign-up")]
        public async Task<IActionResult> SignUp([FromBody]SignUpModel model)
        {
            await _identityService.SignUp(model);

            return NoContent();
        }

        [HttpPost("sign-in")]
        public async Task<IActionResult> SignIn([FromBody]SignInModel model)
            => Ok(await _identityService.SignIn(model));

        
    }
}