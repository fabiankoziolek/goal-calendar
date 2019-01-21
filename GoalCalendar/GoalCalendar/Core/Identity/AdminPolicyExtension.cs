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
