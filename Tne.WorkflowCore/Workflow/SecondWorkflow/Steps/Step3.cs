using Sample.TransactionalOutbox.Persistence;
using System;
using System.Threading.Tasks;
using Tne.WorkflowCore.Services;
using WorkflowCore.Interface;
using WorkflowCore.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Tne.WorkflowCore.Workflow.SecondWorkflow.Steps
{
    public class Step3 : StepBodyAsync
    {
        
        private ILogger _logger;


        public Step3(ILogger<Step3> logger)
        {            
            _logger = logger;
        }

        public string Responsible { get; set; }

        public override async Task<ExecutionResult> RunAsync(IStepExecutionContext context)
        {
            await Task.Delay(10_000);
            _logger.LogInformation($"{context.Workflow.Id} Step3");

            return ExecutionResult.Next();

        }
    }
}