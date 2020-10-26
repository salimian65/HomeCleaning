using System;
using System.Threading.Tasks;
using HomeCleaning.Localization.Resources;
using HomeCleaning.Service;
using HomeCleaning.Service.Contract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace HomeCleaning.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService userService;

        public UserController(UserService userService, IStringLocalizer<SharedResources> localizer) 
        {
            this.userService = userService;
        }

        [HttpPost("Create")]
        public async Task<ActionResult<object>> Create(UserDto dto)
        {
            await userService.Create(dto);
            return new ActionResult<bool>(true);
        }

        [HttpPost("Remove")]
        public async Task<ActionResult<object>> Remove([FromBody] Guid id)
        {
            await userService.Remove(id);
            return new ActionResult<bool>(true);
        }

        [HttpPost("Update")]
        public ActionResult<object> Update()
        {
            throw new NotImplementedException();
        }

        [HttpGet("GetById")]
        public ActionResult<object> GetById(Guid id)
        {
            throw new Exception();
        }

        [HttpGet("GetByUserName")]
        public ActionResult<object> GetByUserName(string username)
        {
            return userService.GetUser(username);
        }

        [HttpPost("Enable")]
        public async Task<ActionResult<object>> Enable([FromBody] Guid id)
        {
            await userService.Enable(id);
            return new ActionResult<bool>(true);
        }
        
        [HttpPost("Disable")]
        public async Task<ActionResult<object>> Disable([FromBody] Guid id)
        {
            await userService.Disable(id);
            return new ActionResult<bool>(true);
        }
    }
}