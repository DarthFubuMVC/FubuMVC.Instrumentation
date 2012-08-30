using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using FubuMVC.Core;
using FubuMVC.Diagnostics.Runtime;

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

        private readonly ConcurrentDictionary<Guid, RouteInstrumentationReport> _instrumentationReports =
            new ConcurrentDictionary<Guid, RouteInstrumentationReport>();

        public InstrumentationReportCache(DiagnosticsSettings settings)
        {
            _settings = settings;
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
                    var report = new RouteInstrumentationReport(_settings, log.ChainId, log.Url);

                    report.AddReportLog(log);

                    return report;
                },
                (guid, report) =>
                {
                    report.AddReportLog(log);
                    return report;
                }
            );
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
