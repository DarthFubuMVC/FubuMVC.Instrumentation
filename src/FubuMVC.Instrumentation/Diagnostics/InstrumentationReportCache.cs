using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using FubuMVC.Core;
using FubuMVC.Core.Urls;
using FubuMVC.Instrumentation.Features.Instrumentation.Models;
using FubuMVC.Instrumentation.Tracing;

namespace FubuMVC.Instrumentation.Diagnostics
{
    public interface IInstrumentationReportCache : IEnumerable<RouteInstrumentationReport>
    {
        RouteInstrumentationReport GetReport(Guid behaviorId);
        void Store(InstrumentationRequestLog log);
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

        public void Store(InstrumentationRequestLog log)
        {
            _instrumentationReports.AddOrUpdate(log.RequestLog.ChainId,
                guid =>
                {
                    var chainId = log.RequestLog.ChainId;
                    var report = new RouteInstrumentationReport(_settings, chainId, log.RequestLog.Url)
                    {
                        ReportUrl = _urls.UrlFor(new InstrumentationInputModel {Id = chainId})
                    };

                    return report.AddReportLog(log);
                },
                (guid, report) =>
                {
                    report.ReportUrl = _urls.UrlFor(new InstrumentationInputModel { Id = log.RequestLog.ChainId });

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
