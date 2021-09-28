using System;
using System.Threading.Tasks;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace Tne.WorkflowCore
{
    public class SearchResponsibleStep : StepBodyAsync
    {
        public string Responsible { get; set; }

        public override async Task<ExecutionResult> RunAsync(IStepExecutionContext context)
        {
            string result = "ivanov@tne.transneft.ru";
            await Task.Delay(1000);
            Responsible = result;
            Console.WriteLine($"Found responsible {Responsible}");
            return ExecutionResult.Next();

        }
    }
}