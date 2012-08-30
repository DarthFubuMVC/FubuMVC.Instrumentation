using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using FubuMVC.Core;
using FubuMVC.Core.Registration;

namespace FubuMVC.Instrumentation.Diagnostics
{
    public interface IInstrumentationReportCache : IEnumerable<RouteInstrumentationReport>
    {
        RouteInstrumentationReport GetReport(Guid behaviorId);
    }

    public class InstrumentationReportCache : IInstrumentationReportCache
    {
        //private readonly IEnumerable<ICacheFilter> _filters;
        private readonly BehaviorGraph _graph;
        private readonly DiagnosticsSettings _settings;
        private readonly ConcurrentDictionary<Guid, RouteInstrumentationReport> _instrumentationReports =
            new ConcurrentDictionary<Guid, RouteInstrumentationReport>();

        public InstrumentationReportCache(/*IEnumerable<ICacheFilter> filters,*/ BehaviorGraph graph, DiagnosticsSettings settings/*, IDebugReportPublisher publisher*/)
        {
            //_filters = filters;
            _graph = graph;
            _settings = settings;
            //publisher.Register(AddReport);
        }

        //private void AddReport(RequestLog debugReport, CurrentRequest request)
        //{
        //    //if (_filters.Any(f => f.Exclude(request)))
        //    //{
        //    //    return;
        //    //}

        //    _instrumentationReports.AddOrUpdate(debugReport.ChainId,
        //        guid =>
        //        {
        //            RouteInstrumentationReport report;
        //            var chain = _graph.Behaviors.SingleOrDefault(c => c.UniqueId == debugReport.ChainId);
        //            if (chain != null && chain.Route != null)
        //            {
        //                report = new RouteInstrumentationReport(_settings, debugReport.ChainId, chain.Route.Pattern);
        //            }
        //            else
        //            {
        //                report = new RouteInstrumentationReport(_settings, debugReport.ChainId);
        //            }

        //            report.AddReportLog(debugReport);
        //            return report;
        //        },
        //        (guid, report) =>
        //        {
        //            report.AddReportLog(debugReport);
        //            return report;
        //        });
        //}

        public IEnumerator<RouteInstrumentationReport> GetEnumerator()
        {
            return _instrumentationReports.Values.GetEnumerator();
        }

        public RouteInstrumentationReport GetReport(Guid behaviorId)
        {
            RouteInstrumentationReport report;
            return _instrumentationReports.TryGetValue(behaviorId, out report) ? report : null;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
