using System.Collections.Generic;
using System.Linq;
using FubuMVC.Core.Urls;
using FubuMVC.Instrumentation.Features.Instrumentation.Models;
using FubuMVC.Instrumentation.Runtime;
using FubuMVC.SlickGrid;

namespace FubuMVC.Instrumentation.Features.Instrumentation
{
    public class RoutesSource : IGridDataSource<RouteInstrumentationModel>
    {
        private readonly IInstrumentationReportCache _cache;
        private readonly IUrlRegistry _urls;

        public RoutesSource(IInstrumentationReportCache cache, IUrlRegistry urls)
        {
            _cache = cache;
            _urls = urls;
        }

        public IEnumerable<RouteInstrumentationModel> GetData()
        {
            return _cache.Select(log => new RouteInstrumentationModel(log){
                ReportUrl = _urls.UrlFor(new InstrumentationRouteDetailsInputModel { Id = log.Id })
                })
                .OrderByDescending(x => x.ExceptionCount)
                .ThenByDescending(x => x.MaxExecution)
                .ToList();
        }
    }
}