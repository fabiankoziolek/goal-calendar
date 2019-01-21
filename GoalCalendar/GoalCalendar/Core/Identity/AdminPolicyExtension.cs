using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace GoalCalendar.Core.Identity
{
    public static class AdminPolicyExtension
    {
        public static void AddAdminPolicy(this IServiceCollection services)
        {
            services.AddAuthorization(options => options.AddPolicy("AdminOnly", policy => policy.RequireClaim("role", "admin")));
        }
    }
}
