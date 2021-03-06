﻿using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using GoalCalendar.UserIdentity.Data.Core.Users;
using IdentityModel.Client;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace GoalCalendar.Core.Identity.Web
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IUserIdentityService _userIdentityService;

        public IdentityController(IMapper mapper, UserManager<User> userManager, IUserIdentityService userIdentityService)
        {
            _mapper = mapper;
            _userIdentityService = userIdentityService;
            _userManager = userManager;
        }

        [HttpGet("{id}")]
        public async Task<User> GetById(int id)
        {
            return await _userIdentityService.GetById(id);
        }
      
        [HttpPost("access")]
        public async Task<ActionResult<JObject>> GetAccessToken(UserRequest userRequest)
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

            return Ok(tokenResponse.AccessToken);
        }

        [HttpPost("refresh/{id}")]
        public async Task<ActionResult<JObject>> RefreshToken(int id)
        {
            // discover endpoints from metadata
            var client = new HttpClient();
            var disco = await client.GetDiscoveryDocumentAsync("http://localhost:5000");
            if (disco.IsError)
            {
                return BadRequest();
            }

            var user = await _userIdentityService.GetById(id);
            var refreshToken = user.RefreshToken;

            var tokenResponse = await client.RequestRefreshTokenAsync(new RefreshTokenRequest()
            {
                Address = disco.TokenEndpoint,
                ClientId = "resourceOwner",
                ClientSecret = "1YAueOC",
                Scope = "goalCalendarApi offline_access",
                RefreshToken = refreshToken,
            });

            return Ok(tokenResponse.AccessToken);
        }

        [HttpPost("register")]
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

            return BadRequest(result.Errors);
        }

        // PUT: api/User/5
        [HttpPut("admin/{userName}")]
        public async Task<IActionResult> SetAdmin(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            await _userManager.AddToRoleAsync(user, "Admin");
            return Ok();
        }
    }
}