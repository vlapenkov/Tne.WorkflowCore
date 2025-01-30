using Sample.TransactionalOutbox.Persistence;
using System;
using System.Threading.Tasks;
using Tne.WorkflowCore.Services;
using WorkflowCore.Interface;
using WorkflowCore.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Tne.WorkflowCore.Workflow.SecondWorkflow.Steps
{
    public class Step4 : StepBodyAsync
    {
        
        //private ILogger _logger;


        //public Step4(ILogger<Step4> logger)
        //{            
        //    _logger = logger;
        //}
                

        public override async Task<ExecutionResult> RunAsync(IStepExecutionContext context)
        {
            await Task.Delay(10_000);
            Console.WriteLine($"{context.Workflow.Id} Step4");

            return ExecutionResult.Next();

        }
    }
}