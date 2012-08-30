using System.Collections.Generic;
using FubuMVC.Diagnostics.Runtime;

namespace FubuMVC.Instrumentation.Features.Instrumentation.Models
{
    public class InstrumentationRouteDetailsModel
    {
        public IList<BehaviorDetailModel> Behaviors { get; set; }
    }

    public class BehaviorDetailModel
    {
        public BehaviorDetailModel(RequestLog report)
        {
            //Name = report.BehaviorType.PrettyPrint();
            //Description = report.Description;
            ExecutionTime = report.ExecutionTime;

            //var visitor = new RecordedRequestBehaviorVisitor();
            //report.Each(x => x.AcceptVisitor(visitor));
            //if (visitor.HasExceptions())
            //{
            //    Exception = visitor.Exceptions().Replace("\r", "<br/>");
            //}
        }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public double ExecutionTime { get; private set; }
        public string Exception { get; set; }
    }
}