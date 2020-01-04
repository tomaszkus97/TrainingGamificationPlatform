using Convey;
using Convey.CQRS.Commands;
using Convey.CQRS.Events;
using Convey.CQRS.Queries;
using Convey.MessageBrokers.CQRS;
using Convey.MessageBrokers.RawRabbit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Trainings.Service.Common;
using Trainings.Service.Contracts;
using Trainings.Service.Events.External;
using Trainings.Service.Repositories;
using Trainings.Service.Services.Clients;

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
            services.AddDbContext<TrainingsDbContext>(options => options.UseLazyLoadingProxies().UseSqlServer("Server=db, 1433;Database=trainingsDb;User=sa;Password=#Tgp-password;"));
            services.AddMvc(option => option.EnableEndpointRouting = false);
            services.AddHttpClient<IPlayersServiceClient, PlayersServiceClient>();
            services.Configure<PlayersServiceClientOptions>(options => Configuration.GetSection("PlayersServiceClientOptions").Bind(options));
            var builder = ConveyBuilder
               .Create(services)
               .AddCommandHandlers()
               .AddQueryHandlers()
               .AddInMemoryQueryDispatcher()
               .AddInMemoryCommandDispatcher()
               .AddInMemoryEventDispatcher()
               .AddRabbitMq<CorrelationContext>()
               .AddEventHandlers();

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
            app.UseRabbitMq()
                .SubscribeEvent<CoachCreatedEvent>()
                .SubscribeEvent<PlayerAssignedToGroupEvent>();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Trainings API v1");
            });
        }
    }
}
