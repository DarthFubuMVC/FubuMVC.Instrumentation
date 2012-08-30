using System.Linq;
using FubuMVC.Instrumentation.Diagnostics;
using FubuMVC.Instrumentation.Features.Instrumentation.Models;

namespace FubuMVC.Instrumentation.Features.Instrumentation.View.Details
{
    public class GetHandler
    {
        private readonly IInstrumentationReportCache _reportCache;

        public GetHandler(IInstrumentationReportCache reportCache)
        {
            _reportCache = reportCache;
        }

        public InstrumentationRouteDetailsModel Execute(InstrumentationRouteDetailsRequestModel inputModel)
        {
            var model = new InstrumentationRouteDetailsModel();
            var report = _reportCache.GetReport(inputModel.Id);

            var debugReport = report.Reports.FirstOrDefault(r => r.Id == inputModel.ReportId);
            if (debugReport != null)
            {
                //model.Behaviors = debugReport.AllSteps().Select(x => new BehaviorDetailModel(x)).ToList();
            }
            return model;
        }
    }
}