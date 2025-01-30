
using Microsoft.Extensions.Hosting;
using Sample.TransactionalOutbox.Persistence;
using Tne.WorkflowCore.Workflow.SecondWorkflow;
using WorkflowCore.Interface;
using WorkflowCore.Services;

namespace Tne.WorkflowCore
{
    public class ExecutorBackgroundService : BackgroundService
    {

        private readonly IServiceProvider _serviceProvider;
        private readonly IWorkflowHost _workflowHost;
        private readonly IWorkflowRepository _workflowRepository;
        private readonly ShopDbContext _shopDbContext;

        private readonly IWorkflowRegistry _workflowRegistry;
        private readonly IConfiguration _configuration;


        public ExecutorBackgroundService(IServiceProvider serviceProvider, IWorkflowHost workflowHost, IWorkflowRepository workflowRepository, IWorkflowRegistry workflowRegistry, IConfiguration configuration)
        {
            _serviceProvider = serviceProvider;
            _workflowHost = workflowHost;
            _workflowRepository = workflowRepository;
            _workflowRegistry = workflowRegistry;
            _configuration = configuration;



        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Yield();
            //await Task.Delay(10_000);

            var defs = _workflowRegistry.GetAllDefinitions();

            using var scope = _serviceProvider.CreateScope();

            var scopedProcessingService = scope.ServiceProvider.GetRequiredService<ShopDbContext>();


            await Task.Delay(10_000);

            Console.WriteLine("BS WF="+ _configuration["ENV_WORKFLOW"]);

            //var instances = await _workflowRepository.GetRunnableInstances(DateTime.MaxValue);

            //if (!instances.Any())
            while (!stoppingToken.IsCancellationRequested)
            {
                {

                    var workflowId = await _workflowHost.StartWorkflow(_configuration["ENV_WORKFLOW"],2);

                    await Task.Delay(60_000, stoppingToken);
                    //var workflowId = await _workflowHost.StartWorkflow(nameof(ApprovalWorkflow));
                    //var workflowId = await _workflowHost.StartWorkflow(nameof(ApprovalWorkflow));
                }

            }
        }

    }
}
