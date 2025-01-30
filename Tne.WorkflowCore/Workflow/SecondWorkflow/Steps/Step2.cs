using System;
using System.Threading.Tasks;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace Tne.WorkflowCore.Workflow.SecondWorkflow.Steps
{
    public class Step2 : StepBodyAsync
    {
        public string Responsible { get; set; }

        public override async Task<ExecutionResult> RunAsync(IStepExecutionContext context)
        {
            await Task.Delay(10_000);
            Console.WriteLine($"{context.Workflow.Id} Step2");
            return ExecutionResult.Next();

        }
    }
}