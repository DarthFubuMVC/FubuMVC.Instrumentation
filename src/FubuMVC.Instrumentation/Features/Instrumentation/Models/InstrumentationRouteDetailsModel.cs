using System.Collections.Generic;
using System.Linq;
using FubuMVC.Instrumentation.Tracing;

namespace FubuMVC.Instrumentation.Features.Instrumentation.Models
{
    public class InstrumentationRouteDetailsModel
    {
        public InstrumentationRouteDetailsModel()
        {
        }
        public InstrumentationRouteDetailsModel(InstrumentationRequestLog log)
        {
            if(log.RequestLog.Failed)
            {
                Exception = log.Exception.Message;
            }
        }
        public IList<BehaviorDetailModel> Behaviors { get; set; }
        public string Exception { get; set; }
    }

    public class BehaviorDetailModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double ExecutionTime { get; set; }
    }
}