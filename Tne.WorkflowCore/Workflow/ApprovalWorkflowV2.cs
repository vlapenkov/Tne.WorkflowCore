using System;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace Tne.WorkflowCore
{
    public class ApprovalWorkflowV2 : IWorkflow
    {
        public string Id => "ApprovalWorkflowV2";

        public int Version => 2;


        public void Build(IWorkflowBuilder<object> builder)
        {
            builder
                .StartWith(context => ExecutionResult.Next())

                .Then<SearchResponsibleStep>()
                //.OnError(WorkflowErrorHandling.Terminate)                    

                .Then<SendMailStep>()

                .Then<SendMailStep>() 

                .Then<ApprovalStep>()
            

            .Then(context =>
            {
                Console.WriteLine("Workflow complete");
                return ExecutionResult.Next();
            });

        }
        public void BuildV2(IWorkflowBuilder<object> builder)
        {

            builder.StartWith(context => ExecutionResult.Next()).
                    Parallel()
                    .Do(then =>
                        then.StartWith<SearchResponsibleStep>()

                            .Then<SendMailStep>())

                    .Do(then =>
                        then.StartWith<SearchResponsibleStep>()

                            .Then<SendMailStep>())
                    .Do(then =>
                        then.StartWith<SearchResponsibleStep>()

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