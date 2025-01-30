using System;
using System.Threading.Tasks;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace Tne.WorkflowCore.Workflow.SecondWorkflow.Steps
{
    public class Step1 : StepBodyAsync
    {
        public string OrganisationName { get; set; }

        public string Url { get; set; }
        public bool? Result { get; set; }


        public override async Task<ExecutionResult> RunAsync(IStepExecutionContext context)
        {

            await Task.Delay(10_000);
            Console.WriteLine($"{context.Workflow.Id} Step1");
            return ExecutionResult.Next();
            

        }
    }
}