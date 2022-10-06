using AutoMapper;
using CustomerManager.Api.Extensions;
using CustomerManager.Application.AutoMapper;
using CustomerManager.Infra.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Threading.Tasks;

namespace CustomerMangerApi
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
            services.AddControllers();
            services.InjectDependencies(Configuration);
            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "V1",
                    Title = "Customer Manager",
                    Description = "Api to manage Cutomer Favorites",
                    Contact = new OpenApiContact
                    {
                        Name = "Elton Alves",
                        Email = "eltim.alves@gmail.com",
                        Url = new System.Uri("https://www.linkedin.com/in/eltonalves/")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "",
                        Url = new System.Uri("https://github.com/EltonAlvess/CustomerManager/blob/main/LICENSE.md"),
                    }
                });
            });

            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new CustomerProfile());
                mc.AddProfile(new FavoriteProfile());
                mc.AddProfile(new ProductProfile());
            });
            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                ApplyPendingMigration(app);
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Customer Manager V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();



            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void ApplyPendingMigration(IApplicationBuilder app)
        {
            using(var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                using(var context = serviceScope.ServiceProvider.GetService<CustomerManagerContext>())
                {
                    context.Database.EnsureCreated();
                }
            }
        }
    }
}
