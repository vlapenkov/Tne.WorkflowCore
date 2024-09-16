using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sample.TransactionalOutbox.Persistence;
using Tne.WorkflowCore.Services;
using Tne.WorkflowCore.Workflow.Middleware;
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

            services.AddScoped<IDependency,Dependency>();
            services.AddTransient<SearchResponsibleStep>();


            // Р В РЎвЂ”Р В Р’ВµР РЋР вЂљР РЋР С“Р В РЎвЂР РЋР С“Р РЋРІР‚С™Р В РЎвЂР В РЎВ Р РЋР С“Р В РЎвЂўР РЋР С“Р РЋРІР‚С™Р В РЎвЂўР РЋР РЏР В Р вЂ¦Р В РЎвЂР РЋР РЏ workflows Р В Р вЂ  MongoDb

            // services.AddWorkflow(x => x.UseMongoDB(@"mongodb://localhost:27017", "workflows"));
            services.AddWorkflow(x =>
            {
                x.UsePostgreSQL(@"Server=127.0.0.1;Port=5432;Database=workflows-new;User Id=postgres;Password=123123;", true, true);
                x.UseErrorRetryInterval(TimeSpan.FromSeconds(100));
                
            }
                );

            //services.AddWorkflowStepMiddleware<PollyRetryMiddleware>();
            services.AddControllers();

            services.AddHostedService<ExecutorBackgroundService>();

            services.AddDbContext<ShopDbContext>(options =>
            {
                options.UseInMemoryDatabase("ShopDb");                    
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

            app.UseEndpoints(endpoints =>
            {
                // endpoints.MapDefaultControllerRoute();
                endpoints.MapControllerRoute(
                     name: "default",
                     pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            // Start the workflow host
            host = app.ApplicationServices.GetService<IWorkflowHost>();
            
            host.RegisterWorkflow<ApprovalWorkflowV2>();

            //var registry = app.ApplicationServices.GetService<IWorkflowRegistry>();

            //var def1 = registry.GetAllDefinitions().First();

            host.Start();

           

            SeedDb.Initialize(app);

        }
    }
}
