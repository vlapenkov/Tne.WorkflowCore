using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Tne.WorkflowCore.Mq;
using WorkflowCore.Interface;

namespace Tne.WorkflowCore
{
    public class ExternalEventConsumer : IConsumer<ApprovalInitDto>

    {
        private static int counter = 0;


        ILogger<ExternalEventConsumer> _logger;
        private readonly IWorkflowHost _workflowHost;

        public ExternalEventConsumer(ILogger<ExternalEventConsumer> logger, IWorkflowHost workflowHost)
        {
            _logger = logger;
            _workflowHost = workflowHost;
        }

        /// <summary>
        /// Обработка события через шину
        /// </summary>

        public async Task Consume(ConsumeContext<ApprovalInitDto> context)
        {
            ApprovalData data = new ApprovalData { OrganisationName = context.Message.OrganisationName, Url = context.Message.Url };

            // Запустить событие ApprovalEvent для ApprovalWorkflow 
            var workflowId = await _workflowHost.StartWorkflow(nameof(ApprovalWorkflow), data);

            _logger.LogInformation("Event consumed: {@Product}", data);

        }


    }
}