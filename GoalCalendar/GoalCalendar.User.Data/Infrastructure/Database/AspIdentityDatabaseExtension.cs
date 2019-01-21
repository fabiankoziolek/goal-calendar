using System;
using System.Collections.Generic;
using System.Text;
using GoalCalendar.UserIdentity.Data.Core.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GoalCalendar.UserIdentity.Data.Infrastructure.Database
{
    public static class AspIdentityDatabaseExtension
    {
        public static void AddAspIdentityDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<UserDbContext>(opt =>
                opt.UseSqlServer(configuration.GetConnectionString("GoalCalendarDb")));

            services.AddIdentityCore<User>(options => { });
            new IdentityBuilder(typeof(User), typeof(IdentityRole<int>), services)
                .AddRoleManager<RoleManager<IdentityRole<int>>>()
                .AddSignInManager<SignInManager<User>>()
                .AddEntityFrameworkStores<UserDbContext>();

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
