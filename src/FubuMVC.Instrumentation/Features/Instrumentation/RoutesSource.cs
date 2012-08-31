using System.Collections.Generic;
using FubuMVC.Core;
using FubuMVC.Core.Urls;
using FubuMVC.Instrumentation.Diagnostics;
using FubuMVC.Instrumentation.Features.Instrumentation.Models;
using FubuMVC.SlickGrid;

namespace FubuMVC.Instrumentation.Features.Instrumentation
{
    public class RoutesSource : IGridDataSource<RouteInstrumentationModel>
    {
        private readonly IInstrumentationReportCache _cache;
        private readonly DiagnosticsSettings _settings;
        private readonly IUrlRegistry _urls;

        public RoutesSource(IInstrumentationReportCache cache, DiagnosticsSettings settings, IUrlRegistry urls)
        {
            _cache = cache;
            _settings = settings;
            _urls = urls;
        }

        public IEnumerable<RouteInstrumentationModel> GetData()
        {
            var viewModel = new List<RouteInstrumentationModel>();

            foreach (var log in _cache)
            {
                var instrumentationReport = new RouteInstrumentationReport(_settings, log.Id);
                foreach (var report in log.Reports)
                {
                    instrumentationReport.AddReportLog(report);
                    instrumentationReport.Url = report.Url;
                    instrumentationReport.ReportUrl = _urls.UrlFor(new InstrumentationInputModel
                    {
                        Id = report.ChainId
                    });
                }
                viewModel.Add(new RouteInstrumentationModel(instrumentationReport));
            }
            return viewModel;
        }
    }
}