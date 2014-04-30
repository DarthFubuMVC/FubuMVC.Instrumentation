using FubuMVC.Core.Diagnostics.Runtime;

namespace FubuMVC.Instrumentation.Runtime
{
    public class InstrumentationRequestNotifier : IRequestTraceObserver
    {
        private readonly IInstrumentationReportCache _cache;

        public InstrumentationRequestNotifier(IInstrumentationReportCache cache)
        {
            _cache = cache;
        }

        public void Started(RequestLog log)
        {
        }

        public void Completed(RequestLog log)
        {
            _cache.Store(log);
        }
    }
}