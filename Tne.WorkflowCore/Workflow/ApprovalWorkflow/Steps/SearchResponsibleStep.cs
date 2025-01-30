using Sample.TransactionalOutbox.Persistence;
using System;
using System.Threading.Tasks;
using Tne.WorkflowCore.Services;
using WorkflowCore.Interface;
using WorkflowCore.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Tne.WorkflowCore.Workflow.ApprovalWorkflow.Steps
{
    public class SearchResponsibleStep : StepBodyAsync
    {

        //private IDependency _dependency;
        //private ShopDbContext _shopDbContext;
        //private ILogger _logger;




        //public SearchResponsibleStep(IDependency dependency, ShopDbContext shopDbContext, ILogger<SearchResponsibleStep> logger)
        //{
        //    _dependency = dependency;
        //    _shopDbContext = shopDbContext;
        //    _logger = logger;
        //}

        public string Responsible { get; set; }

        public override async Task<ExecutionResult> RunAsync(IStepExecutionContext context)
        {
            await Task.Delay(10_000);
            Console.WriteLine($"{context.Workflow.Id} SearchResponsibleStep");

            return ExecutionResult.Next();

        }
    }
}