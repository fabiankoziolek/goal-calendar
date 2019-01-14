using Microsoft.Extensions.DependencyInjection;

namespace GoalCalendar.Utilities.AutomaticDI
{
    public static class AutomaticDI
    {
        public static void ConfigureDependencies(this IServiceCollection services)
        {
            services.Scan(scan => scan
                .FromAssemblyOf<IAssemblyMaker>()
                .AddClasses(classes => classes.AssignableTo<ITransient>())
                .AsImplementedInterfaces()
                .WithTransientLifetime()
                .AddClasses(classes => classes.AssignableTo<IScoped>())
                .AsImplementedInterfaces()
                .WithScopedLifetime()
                .AddClasses(classes => classes.AssignableTo<ISingleton>())
                .AsImplementedInterfaces()
                .WithSingletonLifetime());
        }
    }
}
