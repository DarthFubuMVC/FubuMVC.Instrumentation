using System.Collections.Generic;
using System.Linq;
using FubuMVC.Core.Diagnostics.Runtime;
using FubuMVC.Instrumentation.Runtime;

namespace FubuMVC.Instrumentation.Features.Instrumentation.Models
{
    public class InstrumentationRouteDetailsModel : RouteInstrumentationModel
    {
        public InstrumentationRouteDetailsModel()
        {
            RequestOverviews = new List<InstrumentationRequestOverviewModel>();
        }

        public InstrumentationRouteDetailsModel(RouteInstrumentationReport report) : base(report)
        {
            RequestOverviews = report.Reports
                .OrderByDescending(x => x.Time)
                .Select(x => new InstrumentationRequestOverviewModel
                {
                    Id = x.Id,
                    DateTime = x.Time.ToString(),
                    ExecutionTime = x.ExecutionTime.ToString(),
                    HasException = x.Failed,
                    IsWarning = IsWarning(x)
                });
        }

        public IEnumerable<InstrumentationRequestOverviewModel> RequestOverviews { get; set; }
        public AverageChainModel AverageChain { get; set; }
        public int RequestsCount { get { return RequestOverviews.Count(); } }


        private bool IsWarning(RequestLog report)
        {
            var max = MaxExecution;
            var avg = AverageExecution;
            var p1 = 1 - report.ExecutionTime / max;
            var p2 = 1- (double)avg / max;
            return (p2 - p1) > 0.25;
        }
    }
}
