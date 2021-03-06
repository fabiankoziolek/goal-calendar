using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GoalCalendar.Infrastructure.Database
{
    public static class DatabaseExtension
    {
        public static void AddDatabaseContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<GoalCalendarContext>(opt =>
                opt.UseSqlServer(configuration.GetConnectionString("GoalCalendarDb")));
        }
    }
}