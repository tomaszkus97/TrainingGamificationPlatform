using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Convey;
using Convey.CQRS.Commands;
using Convey.CQRS.Queries;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Trainings.Service.Repositories;


namespace Trainings.Service
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

        }

        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<TrainingsDbContext>(options => options.UseLazyLoadingProxies().UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"]));
            services.AddMvc(option => option.EnableEndpointRouting = false);
            var builder = ConveyBuilder
               .Create(services)
               .AddCommandHandlers()
               .AddQueryHandlers()
               .AddInMemoryQueryDispatcher()
               .AddInMemoryCommandDispatcher();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Trainings API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseHttpsRedirection();
            app.UseMvc();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Players API V1");
            });
        }
    }
}
