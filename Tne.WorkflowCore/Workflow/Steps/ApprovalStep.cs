using System;
using System.Threading.Tasks;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace Tne.WorkflowCore
{
    public class ApprovalStep : StepBodyAsync
    {
        public string OrganisationName { get; set; }

        public string Url { get; set; }
        public bool? Result { get; set; }


        public override async Task<ExecutionResult> RunAsync(IStepExecutionContext context)
        {

            await Task.Delay(1000);
            Console.WriteLine($"ApprovalStep result is {Result}");
            return ExecutionResult.Next();
            // return ExecutionResult.Outcome(Result);

        }
    }
}