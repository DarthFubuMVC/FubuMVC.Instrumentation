using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using FubuCore;
using FubuMVC.Core.Diagnostics.Runtime;
using FubuMVC.Core.Registration;
using FubuMVC.Core.Urls;
using FubuMVC.Diagnostics.Chains;
using FubuMVC.Instrumentation.Features.Instrumentation.Models;

namespace FubuMVC.Instrumentation.Runtime
{
    public class InstrumentationReportCache : IInstrumentationReportCache
    {
        private readonly InstrumentationSettings _settings;
        private readonly IUrlRegistry _urls;
        private readonly BehaviorGraph _graph;

        private readonly ReaderWriterLockSlim _lock = new ReaderWriterLockSlim();

        private readonly IDictionary<Guid, RouteInstrumentationReport> _instrumentationReports =
            new Dictionary<Guid, RouteInstrumentationReport>();

        public InstrumentationReportCache(InstrumentationSettings settings, IUrlRegistry urls, BehaviorGraph graph)
        {
            _settings = settings;
            _urls = urls;
            _graph = graph;
        }

        public IEnumerator<RouteInstrumentationReport> GetEnumerator()
        {
            return _lock.Read(() => _instrumentationReports.Values.GetEnumerator());
        }

        public RouteInstrumentationReport GetReport(Guid behaviorId)
        {
            RouteInstrumentationReport report;
            return _instrumentationReports.TryGetValue(behaviorId, out report) ? report : null;
        }

        public void Store(RequestLog log)
        {
            var id = log.ChainId;
            _lock.Write(() =>
            {
                
                if(_instrumentationReports.ContainsKey(id))
                {
                    var report = _instrumentationReports[id];
                    report.ReportUrl = _urls.UrlFor(new InstrumentationRouteDetailsInputModel { Id = log.ChainId });
                    report.AddReportLog(log);
                }
                else
                {
                    var chain = _graph.Behaviors.FirstOrDefault(x => x.UniqueId == log.ChainId);

                    var title = chain.Title();
                    var report = new RouteInstrumentationReport(_settings, id, title)
                    {
                        ReportUrl = _urls.UrlFor(new InstrumentationRouteDetailsInputModel { Id = id })
                    };

                    report.AddReportLog(log);
                    _instrumentationReports.Add(id, report);
                }
            });
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
