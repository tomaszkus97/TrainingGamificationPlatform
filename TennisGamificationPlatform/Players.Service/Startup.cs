using AutoMapper;
using Convey;
using Convey.CQRS.Commands;
using Convey.CQRS.Events;
using Convey.CQRS.Queries;
using Convey.MessageBrokers.CQRS;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Players.Service.Common;
using Players.Service.Events.External;
using Players.Service.Repositories;
using Convey.MessageBrokers.RawRabbit;

namespace Players.Service
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
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new PlayersServiceMappings());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
            services.AddMvc(option => option.EnableEndpointRouting = false);
            services.AddDbContext<PlayersDbContext>(options => options.UseLazyLoadingProxies().UseSqlServer("Server=db, 1433;Database=playersDb;User=sa;Password=#Tgp-password;"));
            var builder = ConveyBuilder
                .Create(services)
                .AddCommandHandlers()
                .AddQueryHandlers()
                .AddEventHandlers()
                .AddInMemoryEventDispatcher()
                .AddInMemoryQueryDispatcher()
                .AddInMemoryCommandDispatcher()
                .AddRabbitMq<CorrelationContext>();





            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Players API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IMapper mapper)
        {
            if (env.EnvironmentName.Equals("Development"))
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();

            app.UseSwagger();
            app.UseRabbitMq()
                .SubscribeEvent<PlayerCreatedEvent>()
                .SubscribeEvent<PlayerAttendanceEvent>();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Players API V1");
            });
            mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }
    }
}
