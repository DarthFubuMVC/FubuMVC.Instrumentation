using System;
using System.Collections.Generic;
using FubuMVC.Core.Diagnostics.Runtime;

namespace FubuMVC.Instrumentation.Runtime
{
    public interface IInstrumentationReportCache : IEnumerable<RouteInstrumentationReport>
    {
        RouteInstrumentationReport GetReport(Guid behaviorId);
        void Store(RequestLog log);
    }
}