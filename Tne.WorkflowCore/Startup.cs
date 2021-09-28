using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkflowCore.Interface;

namespace Tne.WorkflowCore
{
    public class Startup
    {
        private IWorkflowHost host;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // персистим состояния workflows в MongoDb

            // services.AddWorkflow(x => x.UseMongoDB(@"mongodb://localhost:27017", "workflows"));
            services.AddWorkflow(x => x.UsePostgreSQL(@"Server=127.0.0.1;Port=5432;Database=workflows;User Id=postgres;Password=123123;", true, true));
            services.AddControllers();

            services.AddMassTransit(x =>
            {
                x.AddConsumer<ExternalEventConsumer>();
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host("rabbitmq://localhost");

                    cfg.ReceiveEndpoint("event-listener", e =>
                    {
                        e.ConfigureConsumer<ExternalEventConsumer>(context);
                    });

                });

            });

            services.AddMassTransitHostedService();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                // endpoints.MapDefaultControllerRoute();
                endpoints.MapControllerRoute(
                     name: "default",
                     pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            // Start the workflow host
            host = app.ApplicationServices.GetService<IWorkflowHost>();
            host.RegisterWorkflow<ApprovalWorkflow, ApprovalData>();
            host.Start();

        }
    }
}
