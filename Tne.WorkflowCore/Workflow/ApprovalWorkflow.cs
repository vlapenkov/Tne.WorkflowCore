using System;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace Tne.WorkflowCore
{
    public class ApprovalWorkflow : IWorkflow<ApprovalData>
    {
        public string Id => "ApprovalWorkflow";

        public int Version => 1;

        public void Build(IWorkflowBuilder<ApprovalData> builder)
        {
            builder
                .StartWith(context => ExecutionResult.Next())

                .Then<SearchResponsibleStep>() // поиск ответственного
                    .Output(data => data.ApprovedBy, step => step.Responsible)

                .Then<SendMailStep>() // отправка почты
                    .Input(step => step.Responsible, data => data.ApprovedBy)

                .WaitFor("ApprovalEvent", (data, context) => context.Workflow.Id, data => DateTime.Now) // получение события обработки
                    .Output(data => data.ApprovalStatus, step => step.EventData)

                .Then<ApprovalStep>()
            .Input(step => step.OrganisationName, data => data.OrganisationName)
            .Input(step => step.Url, data => data.Url)
            .Input(step => step.Result, data => data.ApprovalStatus)

            .Then(context =>
             {
                 Console.WriteLine("Workflow complete");
                 return ExecutionResult.Next();
             });
        }
    }
}