using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tne.WorkflowCore.Mq
{
    public class ExternalEvent : IExteralEvent
    {
        public int Id { get; set; }
        public string Data { get; set; }
    }
}
