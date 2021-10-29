using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OrderService.Services;
using System;
using Polly;
using System.Net.Http;
using Polly.Extensions.Http;

namespace OrderService
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
            services.AddHttpClient<IPaymentService, PaymentService>(client =>
            {
                client.BaseAddress = new Uri(Configuration["PaymentServiceUrl"]);
            }).AddPolicyHandler(GetCircuitBreakerPolicy());

            services.AddSwaggerGen(setup => 
            {
                setup.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Order service API", Version = "1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            
            app.UseSwagger().UseSwaggerUI(c => 
            {
                c.SwaggerEndpoint("../swagger/v1/swagger.json", "Order Service API");
            });
        }

        private IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()  //Many faults are transient and may self-correct after a short delay Like 404 
        {
            return HttpPolicyExtensions.HandleTransientHttpError()
                .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
                .WaitAndRetryAsync(5, retryAttempt => TimeSpan.FromSeconds(2));
        }
        private IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .CircuitBreakerAsync(5, TimeSpan.FromSeconds(30));  //Http5XX HTTP408 Request timeout
        }

        private IAsyncPolicy<HttpResponseMessage> GetAdavncedCircuitBreakerPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                 .AdvancedCircuitBreakerAsync(
                    failureThreshold: 0.5, // Break on >=50% actions result in handled exceptions...
                    samplingDuration: TimeSpan.FromSeconds(10), // ... over any 10 second period
                    minimumThroughput: 8, // ... provided at least 8 actions in the 10 second period.
                    durationOfBreak: TimeSpan.FromSeconds(30) // Break for 30 seconds.
                );
        }

        private IAsyncPolicy<HttpResponseMessage> GetAdavncedCircuitBreakerPolicyForAnyException()
        {
            Action<Exception, TimeSpan, Context> onBreak = (exception, timespan,context) => { };
            Action<Context> onReset = (context) => { Console.WriteLine("OnReset "); };
            Action onHalfOpen = () => { };
            var policy= Policy.Handle<Exception>() //Any exception
                 .AdvancedCircuitBreakerAsync(
                    failureThreshold: 0.5, // Break on >=50% actions result in handled exceptions...
                    samplingDuration: TimeSpan.FromSeconds(10), // ... over any 10 second period
                    minimumThroughput: 8, // ... provided at least 8 actions in the 10 second period.
                    durationOfBreak: TimeSpan.FromSeconds(30) // Break for 30 seconds.
                );
           return policy.AsAsyncPolicy<HttpResponseMessage>();
        }
    }
}
