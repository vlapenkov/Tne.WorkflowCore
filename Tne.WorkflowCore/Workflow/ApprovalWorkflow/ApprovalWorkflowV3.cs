using System;
using Tne.WorkflowCore.Workflow.ApprovalWorkflow.Steps;
using Tne.WorkflowCore.Workflow.SecondWorkflow.Steps;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace Tne.WorkflowCore
{
    public class ApprovalWorkflowV3 : IWorkflow
    {
        public string Id => "ApprovalWorkflow";

        public int Version => 3;

        public string Description => "Депозиты, Кредиты, МБК, Прочие счета";

        public void Build(IWorkflowBuilder<object> builder)
        {
            builder
                .StartWith(context => ExecutionResult.Next())

                .Then<SearchResponsibleStep>()               

                .Then<SendMailStep>()               

                .Then<ApprovalStep>()
            

            .Then(context =>
            {
                
                Console.WriteLine($"Workflow {context.Workflow.WorkflowDefinitionId} Version={context.Workflow.Version} complete");
                return ExecutionResult.Next();
            });

        }
        
    }
}