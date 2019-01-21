using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using GoalCalendar.Core.Identity.Web;
using GoalCalendar.UserIdentity.Data.Core.Users;
using IdentityModel.Client;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace GoalCalendar.Core.UserIdentity.Web
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager;
        private readonly IUserService _userService;

        public IdentityController(IMapper mapper, UserManager<User> userManager, IUserService userService, RoleManager<IdentityRole<int>> roleManager)
        {
            _mapper = mapper;
            _userService = userService;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet("{id}")]
        public async Task<User> GetById(int id)
        {
            return await _userService.GetUserById(id);
        }
      
        [HttpGet]
        public async Task<ActionResult<JObject>> Get(UserRequest userRequest)
        {
            // discover endpoints from metadata
            var client = new HttpClient();
            var disco = await client.GetDiscoveryDocumentAsync("http://localhost:5000");
            if (disco.IsError)
            {
                return BadRequest();
            }

            var tokenResponse = await client.RequestPasswordTokenAsync(new PasswordTokenRequest()
            {
                Address = disco.TokenEndpoint,
                ClientId = "resourceOwner",
                ClientSecret = "1YAueOC",
                Scope = "goalCalendarApi offline_access",
                UserName = userRequest.UserName,
                Password = userRequest.Password
            });

            if (!tokenResponse.IsError)
            {
                var user = await _userManager.FindByNameAsync(userRequest.UserName);
                user.UpdateRefreshToken(tokenResponse.RefreshToken);
                await _userManager.UpdateAsync(user);
            }

            return Ok(tokenResponse.Json);
        }

        [HttpGet("/refresh/{id}")]
        public async Task<ActionResult<JObject>> PostRefreshToken(int id)
        {
            // discover endpoints from metadata
            var client = new HttpClient();
            var disco = await client.GetDiscoveryDocumentAsync("http://localhost:5000");
            if (disco.IsError)
            {
                return BadRequest();
            }

            var user = await _userService.GetUserById(id);
            var refreshToken = user.RefreshToken;

            var tokenResponse = await client.RequestRefreshTokenAsync(new RefreshTokenRequest()
            {
                Address = disco.TokenEndpoint,
                ClientId = "resourceOwner",
                ClientSecret = "1YAueOC",
                Scope = "goalCalendarApi offline_access",
                RefreshToken = refreshToken,
            });

            return Ok(tokenResponse.Json);
        }

        [HttpPost]
        public async Task<IActionResult> Post(UserRegistrationRequest userModel)
        {
            var newUser = _mapper.Map<User>(userModel);
            var result = await _userManager.CreateAsync(newUser, userModel.Password);

            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(newUser.UserName);
                // Change this to findById in users controller when it's created
                return CreatedAtAction(nameof(GetById), new { id = user.Id }, userModel);
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return BadRequest();
        }

        // PUT: api/User/5
        [HttpPut("{id}")]
        public async Task<IActionResult> SetAdmin(int id)
        {
            var user = await _userService.GetUserById(id);
            await _userManager.AddToRoleAsync(user, "Admin");
            return Ok();
        }

    }
}