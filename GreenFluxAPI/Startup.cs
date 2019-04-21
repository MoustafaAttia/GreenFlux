using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using GreenFluxAPI.Service;
using GreenFluxAPI.BusinessLogic;

namespace GreenFluxAPI
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            /*services.Add(new ServiceDescriptor(typeof(IPublicHolidaysService), new PublicHolidaysService()));
            services.Add(new ServiceDescriptor(typeof(IPublicHolidaysBL), new PublicHolidaysBL()));*/

            services.AddSingleton<IPublicHolidaysService, PublicHolidaysService>();
            services.AddSingleton(typeof(IPublicHolidaysService), typeof(PublicHolidaysService));

            services.AddSingleton<IPublicHolidaysBL, PublicHolidaysBL>();
            services.AddSingleton(typeof(IPublicHolidaysBL), typeof(PublicHolidaysBL));

            services.AddSwaggerGen(c =>
                 c.SwaggerDoc("v1", new Info() { Title = "GreenFlux Holiday Optimizer Assignment" })
                );

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI( c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "GreenFlux Holiday Optimizer Assignment");
            }
                );

        }
    }
}
