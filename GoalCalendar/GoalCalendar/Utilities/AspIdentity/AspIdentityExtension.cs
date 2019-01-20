using GoalCalendar.Infrastructure.Database;
using GoalCalendar.UserIdentity.Data.Core.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace GoalCalendar.Utilities.AspIdentity
{
    public static class AspIdentityExtension
    {
        public static void AddAspIdentity(this IServiceCollection services)
        {
            services.AddIdentityCore<User>(options => { });
            new IdentityBuilder(typeof(User), typeof(IdentityRole<int>), services)
                .AddRoleManager<RoleManager<IdentityRole<int>>>()
                .AddSignInManager<SignInManager<User>>()
                .AddEntityFrameworkStores<GoalCalendarContext>();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 5;
                options.Password.RequiredUniqueChars = 1;

                options.User.RequireUniqueEmail = true;
            });
        }
    }
}
