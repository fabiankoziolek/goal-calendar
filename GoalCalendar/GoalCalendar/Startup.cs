using GoalCalendar.Infrastructure.AutoMapper;
using GoalCalendar.Infrastructure.Database;
using GoalCalendar.Utilities.AspIdentity;
using GoalCalendar.Utilities.AutomaticDI;
using GoalCalendar.Utilities.Swagger;
using GoalCalendar.Utilities.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GoalCalendar
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddAuthentication("Bearer")
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = "http://localhost:5000";
                    options.RequireHttpsMetadata = false;

                    options.ApiName = "goalCalendarApi";
                });

            services.AddSwagger();
            services.ConfigureDependencies();
            services.AddDatabaseContext(Configuration);
            services.AddAspIdentity(Configuration);
            services.AddMapper();
            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseCustomSwagger();
            }
            else
            {
                app.UseHsts();
            }

            app.UseMiddleware<ExceptionHandlingMiddleware>();
            app.UseHttpsRedirection();
            app.UseMvc();
            app.UseCors(builder => builder.AllowAnyOrigin());
        }
    }
}