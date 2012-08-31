using System.Collections.Generic;
using System.Linq;
using FubuMVC.Instrumentation.Diagnostics;
using FubuMVC.Instrumentation.Features.Instrumentation.Models;
using FubuMVC.SlickGrid;

namespace FubuMVC.Instrumentation.Features.Instrumentation
{
    public class RoutesSource : IGridDataSource<RouteInstrumentationModel>
    {
        private readonly IInstrumentationReportCache _cache;

        public RoutesSource(IInstrumentationReportCache cache)
        {
            _cache = cache;
        }

        public IEnumerable<RouteInstrumentationModel> GetData()
        {
            var reportModels = _cache.Select(log => new RouteInstrumentationModel(log));

            return reportModels;
        }
    }
}