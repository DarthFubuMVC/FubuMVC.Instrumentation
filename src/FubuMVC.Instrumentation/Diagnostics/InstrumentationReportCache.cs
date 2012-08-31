using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using FubuMVC.Core;
using FubuMVC.Core.Urls;
using FubuMVC.Diagnostics.Runtime;
using FubuMVC.Instrumentation.Features.Instrumentation.Models;

namespace FubuMVC.Instrumentation.Diagnostics
{
    public interface IInstrumentationReportCache : IEnumerable<RouteInstrumentationReport>
    {
        RouteInstrumentationReport GetReport(Guid behaviorId);
        void Store(RequestLog log);
    }

    public class InstrumentationReportCache : IInstrumentationReportCache
    {
        private readonly DiagnosticsSettings _settings;
        private readonly IUrlRegistry _urls;

        private readonly ConcurrentDictionary<Guid, RouteInstrumentationReport> _instrumentationReports =
            new ConcurrentDictionary<Guid, RouteInstrumentationReport>();

        public InstrumentationReportCache(DiagnosticsSettings settings, IUrlRegistry urls)
        {
            _settings = settings;
            _urls = urls;
        }

        public IEnumerator<RouteInstrumentationReport> GetEnumerator()
        {
            return _instrumentationReports.Values.GetEnumerator();
        }

        public RouteInstrumentationReport GetReport(Guid behaviorId)
        {
            RouteInstrumentationReport report;
            return _instrumentationReports.TryGetValue(behaviorId, out report) ? report : null;
        }

        public void Store(RequestLog log)
        {
            _instrumentationReports.AddOrUpdate(log.ChainId,
                guid =>
                {
                    var report = new RouteInstrumentationReport(_settings, log.ChainId, log.Url)
                    {
                        ReportUrl = _urls.UrlFor(new InstrumentationInputModel {Id = log.ChainId})
                    };

                    return report.AddReportLog(log);
                },
                (guid, report) =>
                {
                    report.ReportUrl = _urls.UrlFor(new InstrumentationInputModel { Id = log.ChainId });

                    return report.AddReportLog(log);
                }
            );
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
