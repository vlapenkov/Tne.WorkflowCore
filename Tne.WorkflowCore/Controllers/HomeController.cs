using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tne.WorkflowCore.Models;
using WorkflowCore.Interface;
using WorkflowCore.Models;

using static WorkflowCore.Models.WorkflowStatus;

namespace Tne.WorkflowCore
{
    [Route("[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IWorkflowHost workflowHost;
        private readonly IWorkflowRepository workflowRepository;
        readonly IPublishEndpoint _publishEndpoint;


        public HomeController(IWorkflowHost workflowHost, IWorkflowRepository workflowRepository, IPublishEndpoint publishEndpoint)
        {
            this.workflowHost = workflowHost;
            this.workflowRepository = workflowRepository;
            _publishEndpoint = publishEndpoint;
        }

        /// <summary>
        /// Получить все активные workflows
        /// </summary>        
        // [HttpGet("Index")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var instances = await workflowRepository.GetWorkflowInstances(WorkflowStatus.Runnable, null, DateTime.Now.Date, DateTime.Now.Date.AddDays(1), 0, 10);
            return Ok(instances);
        }

        /// <summary>
        /// Запустить workflow ApprovalWorkflow
        /// </summary>
        [HttpPost("Init")]
        public async Task<IActionResult> Init(ApprovalInitDto dto)
        {

            ApprovalData data = new ApprovalData { OrganisationName = dto.OrganisationName, Url = dto.Url };
            var workflowId = await workflowHost.StartWorkflow(nameof(ApprovalWorkflow), data);

            return Ok(workflowId);
        }

        /// <summary>
        /// Запустить событие ApprovalEvent для ApprovalWorkflow 
        /// </summary>
        [HttpPost("Approve")]
        public async Task<IActionResult> Approve(ApprovalFinalDto dto)
        {
            await workflowHost.PublishEvent("ApprovalEvent", dto.WorkflowId, dto.ApprovalStatus);

            return Ok();
        }

        /// <summary>
        /// Запустить событие в шину, а оттуда уже ApprovalEvent для ApprovalWorkflow 
        /// </summary>
        [HttpPost("PublishInit")]
        public async Task PublishInit(ApprovalInitDto dto)
        {
            await _publishEndpoint.Publish<ApprovalInitDto>(dto);

        }

    }
}
