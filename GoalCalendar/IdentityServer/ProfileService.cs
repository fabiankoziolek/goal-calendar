using System;
using System.Collections.Generic;
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
        private readonly IUserIdentityService _userIdentityService;
        private readonly UserManager<User> _userManager;

        public ProfileService(IUserIdentityService userIdentityService, UserManager<User> userManager)
        {
            _userIdentityService = userIdentityService;
            _userManager = userManager;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var sub = context.Subject.GetSubjectId();
            var subNumber = Convert.ToInt32(sub);

            var user = await _userIdentityService.GetById(subNumber);

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
