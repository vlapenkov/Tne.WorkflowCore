
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
        


        public HomeController(IWorkflowHost workflowHost, IWorkflowRepository workflowRepository)
        {
            this.workflowHost = workflowHost;
            this.workflowRepository = workflowRepository;
            
        }

        /// <summary>
        /// РџРѕР»СѓС‡РёС‚СЊ РІСЃРµ Р°РєС‚РёРІРЅС‹Рµ workflows
        /// </summary>        
        // [HttpGet("Index")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var instances = await workflowRepository.GetWorkflowInstances(WorkflowStatus.Runnable, null, DateTime.UtcNow.Date, DateTime.UtcNow.Date.AddDays(1), 0, 10);
            return Ok(instances);
        }

        /// <summary>
        /// Р—Р°РїСѓСЃС‚РёС‚СЊ workflow ApprovalWorkflow
        /// </summary>
        [HttpGet("Init")]
        private async Task<IActionResult> Init()
        {
            
            ApprovalData data = new ApprovalData { OrganisationName = "Организация1", Url = "https://gsdhf.ru" };

            
            //var workflowId = await workflowHost.StartWorkflow(nameof(ApprovalWorkflowV2));

            var workflowId = await workflowHost.StartWorkflow("ApprovalWorkflow", 2);

            return Ok(workflowId);
        }

        /// <summary>
        /// Р—Р°РїСѓСЃС‚РёС‚СЊ СЃРѕР±С‹С‚РёРµ ApprovalEvent РґР»СЏ ApprovalWorkflow 
        /// </summary>
        [HttpPost("Approve")]
        public async Task<IActionResult> Approve(ApprovalFinalDto dto)
        {
            await workflowHost.PublishEvent("ApprovalEvent", dto.WorkflowId, dto.ApprovalStatus);

            return Ok();
        }

        /// <summary>
        /// Р—Р°РїСѓСЃС‚РёС‚СЊ СЃРѕР±С‹С‚РёРµ РІ С€РёРЅСѓ, Р° РѕС‚С‚СѓРґР° СѓР¶Рµ ApprovalEvent РґР»СЏ ApprovalWorkflow 
        /// </summary>
        [HttpPost("PublishInit")]
        public async Task PublishInit(ApprovalInitDto dto)
        {
         //   await _publishEndpoint.Publish<ApprovalInitDto>(dto);

        }

    }
}
