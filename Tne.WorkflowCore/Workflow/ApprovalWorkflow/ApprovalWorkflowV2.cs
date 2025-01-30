using System;
using Tne.WorkflowCore.Workflow.SecondWorkflow.Steps;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace Tne.WorkflowCore
{
    public class ApprovalWorkflowV2 : IWorkflow
    {
        public string Id => "ApprovalWorkflow";

        public int Version => 2;

        public string? Description => throw new NotImplementedException();


        //public string? Description => "Депозиты, Кредиты, МБК";


        public void Build(IWorkflowBuilder<object> builder)
        {
            builder
                .StartWith(context => ExecutionResult.Next())

                .Then<Step3>()
                //.OnError(WorkflowErrorHandling.Terminate)                    

                .Then<SendMailStep>()

                .Then<SendMailStep>() 

                .Then<ApprovalStep>()
            

            .Then(context =>
            {
                
                Console.WriteLine($"Workflow {context.Workflow.WorkflowDefinitionId} Version={context.Workflow.Version} complete");
                return ExecutionResult.Next();
            });

        }
        public void BuildV2(IWorkflowBuilder<object> builder)
        {

            builder.StartWith(context => ExecutionResult.Next()).
                    Parallel()
                    .Do(then =>
                        then.StartWith<Step3>()

                            .Then<SendMailStep>())

                    .Do(then =>
                        then.StartWith<Step3>()

                            .Then<SendMailStep>())
                    .Do(then =>
                        then.StartWith<Step3>()

                            .Then<SendMailStep>())
                .Join()

            .Then(context =>
             {
                 Console.WriteLine("Workflow complete");
                 return ExecutionResult.Next();
             });
        }
    }
}