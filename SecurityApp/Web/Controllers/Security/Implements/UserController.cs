using Entity.DTO;
using Entity.DTO.Security;
using Microsoft.AspNetCore.Mvc;
using Service.Security.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;
using Web.Controllers.Security.Interface;

namespace Web.Controller.Implements
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase, IUserController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAll()
        {
            var users = await _userService.GetAll();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetById(int id)
        {
            var user = await _userService.GetById(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> Add(UserDto userDto)
        {
            await _userService.Add(userDto);
            return CreatedAtAction(nameof(GetById), new { id = userDto.Id }, userDto);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserPersonRoleDto userPersonRoleDto) // Modificado para incluir el DTO
        {
            await _userService.AddUserAndPerson(userPersonRoleDto.User, userPersonRoleDto.Person, userPersonRoleDto.RoleId); // Añadir roleId
            return CreatedAtAction(nameof(GetById), new { id = userPersonRoleDto.User.Id }, userPersonRoleDto.User);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(UserDto userDto)
        {
            await _userService.Update(userDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _userService.Delete(id);
            return NoContent();
        }
    }
}
