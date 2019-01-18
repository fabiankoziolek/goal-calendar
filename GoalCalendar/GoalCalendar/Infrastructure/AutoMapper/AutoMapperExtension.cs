using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace GoalCalendar.Infrastructure.AutoMapper
{
    public static class AutoMapperExtension
    {
        public static void AddMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(configuration => { configuration.ValidateInlineMaps = false; });
        }
    }
}