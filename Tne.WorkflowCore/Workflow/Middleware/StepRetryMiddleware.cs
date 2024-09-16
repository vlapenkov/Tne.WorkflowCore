using Polly;
using WorkflowCore.Interface;
using WorkflowCore.Models;
using WorkflowCore.Services;
using static System.Net.Mime.MediaTypeNames;

namespace Tne.WorkflowCore.Workflow.Middleware
{
    public class PollyRetryMiddleware : IWorkflowStepMiddleware
    {
        private const string StepContextKey = "WorkflowStepContext";
        private const int MaxRetries = 3;
        private readonly ILogger<PollyRetryMiddleware> _log;
        private readonly IPersistenceProvider _persistenceStore;

        public PollyRetryMiddleware(ILogger<PollyRetryMiddleware> log, IPersistenceProvider persistenceStore)
        {
            _log = log;
            _persistenceStore = persistenceStore;
        }

        public IAsyncPolicy<ExecutionResult> GetRetryPolicy(string workflowId) =>
            Policy<ExecutionResult>
                //.Handle<TimeoutException>()
                .Handle<Exception>()
                .RetryAsync(
                    MaxRetries,
                    async (result, retryCount, context) =>
                       await UpdateRetryCount(result.Exception, retryCount, context[StepContextKey] as IStepExecutionContext, workflowId)
                );

        public async Task<ExecutionResult> HandleAsync(
            IStepExecutionContext context,
            IStepBody body,
            WorkflowStepDelegate next
        )
        {

            string workflowId = context.Workflow.Id;

            return await GetRetryPolicy(workflowId).ExecuteAsync(ctx => next(), new Dictionary<string, object>
            {
                { StepContextKey, context }
            });
        }

        private async Task UpdateRetryCount(
            Exception exception,
            int retryCount,
            IStepExecutionContext stepContext, string workflowId)
        {
            var stepInstance = stepContext.ExecutionPointer;
            stepInstance.RetryCount = retryCount;

            _log.LogWarning(
                exception,
                "Exception occurred in step {StepId}. Retrying [{RetryCount}/{MaxCount}]",
                stepInstance.Id,
                retryCount,
                MaxRetries
            );


            //if (retryCount == MaxRetries)
            //// TODO: Come up with way to persist workflow
            //{
            //    var wf = await _persistenceStore.GetWorkflowInstance(workflowId);

            //    wf.Status = WorkflowStatus.Terminated;
            //    wf.CompleteTime = DateTime.UtcNow;

            //    await _persistenceStore.PersistWorkflow(wf);
            //    //return Task.CompletedTask;
            //}

            
        }
    }
}
