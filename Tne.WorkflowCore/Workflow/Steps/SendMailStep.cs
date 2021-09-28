using System;
using System.Threading.Tasks;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace Tne.WorkflowCore
{
    public class SendMailStep : StepBodyAsync
    {
        public string Responsible { get; set; }

        public override async Task<ExecutionResult> RunAsync(IStepExecutionContext context)
        {
            await Task.Delay(1000);
            Console.WriteLine($"Sent e-mail to responsible {Responsible}");
            return ExecutionResult.Next();

        }
    }
}