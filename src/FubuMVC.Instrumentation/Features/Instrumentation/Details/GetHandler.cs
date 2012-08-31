using System.Linq;
using FubuCore;
using FubuMVC.Diagnostics.Visualization;
using FubuMVC.Instrumentation.Diagnostics;
using FubuMVC.Instrumentation.Features.Instrumentation.Models;

namespace FubuMVC.Instrumentation.Features.Instrumentation.Details
{
    public class GetHandler
    {
        private readonly IInstrumentationReportCache _reportCache;
        private readonly IVisualizer _visualizer;

        public GetHandler(IInstrumentationReportCache reportCache, IVisualizer visualizer)
        {
            _reportCache = reportCache;
            _visualizer = visualizer;
        }

        public InstrumentationRouteDetailsModel Execute(InstrumentationRouteDetailsRequestModel inputModel)
        {
            var report = _reportCache.GetReport(inputModel.Id);
            var log = report.Reports.FirstOrDefault(r => r.RequestLog.Id == inputModel.ReportId);

            var viewModel = new InstrumentationRouteDetailsModel(log);

            viewModel.Behaviors = log.RequestLog.AllSteps().Select(x =>
            {
                var desc = _visualizer.ToVisualizationSubject(x.Log).As<CollapsedDescription>();

                return new BehaviorDetailModel
                {
                    Name = desc.Description.Title,
                    Description = desc.Description.ShortDescription,
                    ExecutionTime = x.RequestTimeInMilliseconds
                };
            }).ToList();

            return viewModel;
        }
    }
}