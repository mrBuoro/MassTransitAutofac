using System;
using System.Reflection;
using Library;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            // Configura MassTransit sulla coda rabbitmq
            services.AddMassTransit(x => {

                //x.AddConsumer<CheckOrderStatusConsumer>();
                x.AddConsumers(Assembly.GetExecutingAssembly());

                x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg => {

                    var host = cfg.Host(new Uri($"rabbitmq://rabbit"), hostConfig => {
                        hostConfig.Username("guest");
                        hostConfig.Password("guest");
                    });

                    cfg.ConfigureEndpoints(provider);
                }));
            });

            //
            services.AddMassTransitHostedService();




            // ================================================================================================================
            // Standard configurations

            //
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            //
            return services.StdAutofac();
        }






        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
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
        }
    }
}
