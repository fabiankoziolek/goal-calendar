using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace GoalCalendar.UserIdentity.Data.Core.Users.Web
{
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;
        private readonly IMapper _mapper;

        public UsersController(IUsersService usersService, IMapper mapper)
        {
            _usersService = usersService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<UserResponse> GetById(int id)
        {
            var user = await _usersService.GetById(id);
            return _mapper.Map<UserResponse>(user);
        }

        [HttpPost]
        public async Task<IActionResult> Add(UserRequest userRequest)
        {
            var newUser = _mapper.Map<User>(userRequest);
            await _usersService.Add(newUser);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(UserRequest user, int id)
        {
            var userToUpdate = _mapper.Map<User>(user);
            await _usersService.Update(userToUpdate, id);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _usersService.Delete(id);
            return NoContent();
        }
    }
}