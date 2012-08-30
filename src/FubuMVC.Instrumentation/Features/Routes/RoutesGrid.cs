using System.Collections.Generic;
using System.Linq;
using FubuMVC.Core;
using FubuMVC.Diagnostics.Runtime;
using FubuMVC.Instrumentation.Diagnostics;
using FubuMVC.Instrumentation.Features.Routes.Models;
using FubuMVC.SlickGrid;

namespace FubuMVC.Instrumentation.Features.Routes
{
    public class RoutesGrid : GridDefinition<RouteInstrumentationModel>
    {
        public RoutesGrid()
        {
            SourceIs<RoutesSource>();
            Column(x => x.Url).Title("Route");
            Column(x => x.AverageExecution).Title("Average Execution Time (ms)");
            Column(x => x.HitCount).Title("Hit Count");
            Column(x => x.ExceptionCount).Title("Exception Count");
            Column(x => x.MinExecution).Title("Min Execution Time (ms)");
            Column(x => x.MaxExecution).Title("Max Execution Time (ms)");
            Data(x => x.Id);
        }
    }

    public class RoutesSource : IGridDataSource<RouteInstrumentationModel>
    {
        private readonly IRequestHistoryCache _cache;
        private readonly DiagnosticsSettings _settings;

        public RoutesSource(IRequestHistoryCache cache, DiagnosticsSettings settings)
        {
            _cache = cache;
            _settings = settings;
        }

        public IEnumerable<RouteInstrumentationModel> GetData()
        {
            //TODO: Actually tie into the tracing instead of calculating this on each request
            //This was just to get something to the screen...
            var retVal = new List<RouteInstrumentationModel>();
            var groupedReports = _cache.RecentReports().GroupBy(r => r.ChainId);
            foreach (var chain in groupedReports)
            {
                var instrumentationReport = new RouteInstrumentationReport(_settings, chain.Key);
                foreach (var report in chain)
                {
                    instrumentationReport.AddReportLog(report);
                    instrumentationReport.Url = report.Url;
                }
                retVal.Add(new RouteInstrumentationModel(instrumentationReport));
            }
            return retVal;
        }
    }
}