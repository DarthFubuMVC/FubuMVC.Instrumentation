using System.Linq;
using FubuMVC.Instrumentation.Diagnostics;

namespace FubuMVC.Instrumentation.Features.Instrumentation.Models
{
    public class InstrumentationCacheModelBuilder //: IModelBuilder<InstrumentationCacheModel>
    {
        private readonly IInstrumentationReportCache _instrumentationCache;

        public InstrumentationCacheModelBuilder(IInstrumentationReportCache instrumentationCache)
        {
            _instrumentationCache = instrumentationCache;
        }

        public InstrumentationCacheModel Build()
        {
            return new InstrumentationCacheModel
            {
                RouteInstrumentations = _instrumentationCache.Select(r => new RouteInstrumentationModel
                {
                    Id = r.Id,
                    Url = r.Url,
                    HitCount = r.HitCount,
                    AverageExecution = r.AverageExecution,
                    MaxExecution = r.MaxExecution,
                    MinExecution = r.MinExecution,
                    ExceptionCount = r.ExceptionCount
                }).ToList()
            };
        }
    }
}