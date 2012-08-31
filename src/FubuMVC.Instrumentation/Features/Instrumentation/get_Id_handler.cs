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

        public get_Id_handler(IInstrumentationReportCache reportCache,
           IAverageChainVisualizerBuilder averagesBuilder
            )
        {
            _reportCache = reportCache;
            _averagesBuilder = averagesBuilder;
        }

        public InstrumentationDetailsModel Execute(InstrumentationInputModel inputModel)
        {
            var report = _reportCache.GetReport(inputModel.Id);

            if (report == null)
                return new InstrumentationDetailsModel{Id = Guid.Empty};

            var model = new InstrumentationDetailsModel
            {
                Id = report.Id,
                Url = report.Url,
                AverageExecution = report.AverageExecution,
                ExceptionCount = report.ExceptionCount,
                HitCount = report.HitCount,
                MaxExecution = report.MaxExecution,
                MinExecution = report.MinExecution,
                AverageChain = _averagesBuilder.VisualizerFor(inputModel.Id)
            };

            model.RequestOverviews.AddRange(report.Reports
                .OrderByDescending(x => x.Time)
                .Select(x =>
                {
                    //var visitor = new RecordedRequestBehaviorVisitor();
                    //x.Steps.Each(s => s.Details.AcceptVisitor(visitor));
                    return new InstrumentationRequestOverviewModel
                    {
                        Id = x.Id,
                        DateTime = x.Time.ToString(),
                        ExecutionTime = x.ExecutionTime.ToString(),
                        //HasException = visitor.HasExceptions(),
                        IsWarning = IsWarning(model, x)
                    };
                }));

            return model;
        }

        private bool IsWarning(InstrumentationDetailsModel model, RequestLog report)
        {
            var max = model.MaxExecution;
            var avg = model.AverageExecution;
            var p1 = 1 - (double)report.ExecutionTime / max;
            var p2 = 1- (double)avg / max;
            return (p2 - p1) > 0.25;
        }
    }
}
