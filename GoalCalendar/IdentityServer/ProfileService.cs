using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using GoalCalendar.UserIdentity.Data.Core.Users;
using IdentityModel;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;

namespace IdentityServer
{
    public class ProfileService : IProfileService
    {
        private readonly IUserService _userService;
        private readonly UserManager<User> _userManager;

        public ProfileService(IUserService userService, UserManager<User> userManager)
        {
            _userService = userService;
            _userManager = userManager;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var sub = context.Subject.GetSubjectId();
            var subNumber = Convert.ToInt32(sub);

            var user = await _userService.GetUserById(subNumber);

            var claims = await GetUserClaims(user);

            context.IssuedClaims = claims;
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            context.IsActive = true;
        }

        private async Task<List<Claim>> GetUserClaims(User user)
        {
            var claims = new List<Claim> { new Claim(JwtClaimTypes.Email, user.Email) };

            if (await _userManager.IsInRoleAsync(user, "Admin"))
            {
                claims.Add(new Claim(JwtClaimTypes.Role, "Admin"));
            }

            return claims;
        }
    }
}
