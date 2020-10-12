using System;
using System.Threading.Tasks;
using Localization.Resources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Services;
using Services.Dto.Recipients;
using Services.Dto.Security;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService userService;

        public UserController(UserService userService, IStringLocalizer<SharedResources> localizer) : base(localizer)
        {
            this.userService = userService;
        }

        [HttpPost("Create")]
        public async Task<ActionResult<object>> Create(UserManipulationDto userManipulationDto)
        {
            await userService.Create(userManipulationDto);
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

        [HttpGet("GetByPersonId")]
        public ActionResult<object> GetByPersonId(int id)
        {
            throw new Exception();
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