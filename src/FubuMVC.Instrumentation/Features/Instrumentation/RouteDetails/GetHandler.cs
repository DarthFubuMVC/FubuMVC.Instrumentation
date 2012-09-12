using System;
using FubuMVC.Instrumentation.Chains;
using FubuMVC.Instrumentation.Features.Instrumentation.Models;
using FubuMVC.Instrumentation.Runtime;

namespace FubuMVC.Instrumentation.Features.Instrumentation.RouteDetails
{
    public class get_Id_Handler
    {
        private readonly IInstrumentationReportCache _reportCache;
        private readonly IAverageChainVisualizerBuilder _averagesBuilder;

        public get_Id_Handler(IInstrumentationReportCache reportCache, IAverageChainVisualizerBuilder averagesBuilder)
        {
            _reportCache = reportCache;
            _averagesBuilder = averagesBuilder;
        }

        public InstrumentationRouteDetailsModel Execute(InstrumentationRouteDetailsInputModel inputModel)
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
