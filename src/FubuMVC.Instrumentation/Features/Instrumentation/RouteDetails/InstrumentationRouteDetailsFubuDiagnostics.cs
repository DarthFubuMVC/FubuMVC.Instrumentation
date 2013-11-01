using System;
using FubuMVC.Core;
using FubuMVC.Core.Behaviors.Chrome;
using FubuMVC.Diagnostics.Chrome;
using FubuMVC.Instrumentation.Chains;
using FubuMVC.Instrumentation.Features.Instrumentation.Models;
using FubuMVC.Instrumentation.Runtime;

namespace FubuMVC.Instrumentation.Features.Instrumentation.RouteDetails
{
    public class InstrumentationRouteDetailsFubuDiagnostics
    {
        private readonly IInstrumentationReportCache _reportCache;
        private readonly IAverageChainVisualizerBuilder _averagesBuilder;

        public InstrumentationRouteDetailsFubuDiagnostics(IInstrumentationReportCache reportCache, IAverageChainVisualizerBuilder averagesBuilder)
        {
            _reportCache = reportCache;
            _averagesBuilder = averagesBuilder;
        }

        [Chrome(typeof (DashboardChrome), Title = "Instrumentation Route Details")]
        public InstrumentationRouteDetailsModel get_instrumentation_route_details_Id(InstrumentationRouteDetailsInputModel inputModel)
        {
            var report = _reportCache.GetReport(inputModel.Id);

            if (report == null) return new InstrumentationRouteDetailsModel{Id = Guid.Empty};

            return new InstrumentationRouteDetailsModel(report)
            {
                AverageChain = _averagesBuilder.VisualizerFor(inputModel.Id)
            };
        }
    }
}
