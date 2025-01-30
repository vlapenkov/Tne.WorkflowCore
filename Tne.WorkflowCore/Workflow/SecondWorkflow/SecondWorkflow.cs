using System;
using Tne.WorkflowCore.Workflow.SecondWorkflow.Steps;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace Tne.WorkflowCore.Workflow.SecondWorkflow
{
    public class SecondWorkflow : IWorkflow
    {
        public string Id => "SecondWorkflow";

        public int Version => 1;

        public string Description => "Описание SecondWorkflow";

        public void Build(IWorkflowBuilder<object> builder)
        {
            builder
                .StartWith<Step1>()
                .Then<Step2>()
                .Then<Step3>()
                .Then<Step4>()

            .Then(context =>
             {
                 Console.WriteLine("Workflow SecondWorkflow complete");
                 return ExecutionResult.Next();
             });

        }
    }
}