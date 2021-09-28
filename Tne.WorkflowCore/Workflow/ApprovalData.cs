using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tne.WorkflowCore
{
    public class ApprovalData
    {
        public string OrganisationName { get; set; }

        public string Url { get; set; }

        public bool? ApprovalStatus { get; set; }

        public string ApprovedBy { get; set; }
    }
}
