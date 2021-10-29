using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discount.Api.Protos;
using Basket.API.Services;
using Basket.API.Repositories.Interfaces;
using Basket.API.Repositories;
using MassTransit;
using Steeltoe.Discovery.Client;
using Steeltoe.Discovery.Eureka;

namespace Basket.API
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

            services.AddAutoMapper(typeof(Startup));
            services.AddScoped<IBasketRepository, BasketRepository>();

            //Eureka
              services.AddDiscoveryClient(Configuration);
              services.AddHealthChecks();

            ////End



            ////GRPC
             services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(client => { client.Address = new Uri("http://localhost:5001"); });
             services.AddScoped<DiscountGrpcService>();
            ////End


            ////Mass transit

            services.AddMassTransit(config =>
            {
                config.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host(Configuration["EventBus:HostAddress"]);
                    cfg.UseHealthCheck(ctx); //configure the health check for the bus instance
                });
            });
            services.AddMassTransitHostedService();

            //End

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Basket.API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Basket.API v1"));
            }

            //Eureka

              app.UseDiscoveryClient();
              app.UseHealthChecks("/info");

            //End

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
