
using Microsoft.Extensions.Hosting;
using Sample.TransactionalOutbox.Persistence;
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

        private readonly IWorkflowRegistry  _workflowRegistry;

        public ExecutorBackgroundService(IServiceProvider serviceProvider, IWorkflowHost workflowHost, IWorkflowRepository workflowRepository, IWorkflowRegistry workflowRegistry)
        {
            _serviceProvider = serviceProvider;
            _workflowHost = workflowHost;
            _workflowRepository = workflowRepository;
            _workflowRegistry = workflowRegistry;




        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Yield();
            //await Task.Delay(10_000);

            var defs =  _workflowRegistry.GetAllDefinitions();

            using var scope = _serviceProvider.CreateScope();
            
            var scopedProcessingService = scope.ServiceProvider.GetRequiredService<ShopDbContext>();
                       
            
           

            var instances = await _workflowRepository.GetRunnableInstances(DateTime.MaxValue);

            if (!instances.Any())
            {
                var workflowId = await _workflowHost.StartWorkflow(nameof(ApprovalWorkflowV2));
            }

        }
    }
    
}
