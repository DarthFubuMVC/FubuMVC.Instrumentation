using System;
using System.Linq;
using System.Collections.Generic;
using FubuMVC.Diagnostics.Runtime;
using FubuMVC.Instrumentation.Diagnostics;
using FubuMVC.Instrumentation.Features.Instrumentation.Models;

namespace FubuMVC.Instrumentation.Features.Instrumentation
{
    public class get_Id_handler
    {
        private readonly IInstrumentationReportCache _reportCache;
        private readonly IAverageChainVisualizerBuilder _averagesBuilder;

        public get_Id_handler(IInstrumentationReportCache reportCache, IAverageChainVisualizerBuilder averagesBuilder)
        {
            _reportCache = reportCache;
            _averagesBuilder = averagesBuilder;
        }

        public InstrumentationDetailsModel Execute(InstrumentationInputModel inputModel)
        {
            var report = _reportCache.GetReport(inputModel.Id);

            if (report == null)
                return new InstrumentationDetailsModel{Id = Guid.Empty};

            var viewModel = new InstrumentationDetailsModel(report)
            {
                AverageChain = _averagesBuilder.VisualizerFor(inputModel.Id)
            };

            viewModel.RequestOverviews.AddRange(report.Reports
                .OrderByDescending(x => x.RequestLog.Time)
                .Select(x => new InstrumentationRequestOverviewModel
                {
                    Id = x.RequestLog.Id,
                    DateTime = x.RequestLog.Time.ToString(),
                    ExecutionTime = x.RequestLog.ExecutionTime.ToString(),
                    HasException = x.RequestLog.Failed,
                    IsWarning = IsWarning(viewModel, x.RequestLog)
                }));

            return viewModel;
        }

        private static bool IsWarning(RouteInstrumentationModel model, RequestLog report)
        {
            var max = model.MaxExecution;
            var avg = model.AverageExecution;
            var p1 = 1 - report.ExecutionTime / max;
            var p2 = 1- (double)avg / max;
            return (p2 - p1) > 0.25;
        }
    }
}
