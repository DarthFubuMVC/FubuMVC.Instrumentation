using FubuMVC.Diagnostics.Runtime;

namespace FubuMVC.Instrumentation.Runtime
{
    public class InstrumentationRequestNotifier : IRequestTraceNotifier
    {
        private readonly IInstrumentationReportCache _cache;

        public InstrumentationRequestNotifier(IInstrumentationReportCache cache)
        {
            _cache = cache;
        }

        public void OnStart(RequestLog log)
        {
        }

        public void OnComplete(RequestLog log)
        {
            _cache.Store(log);
        }
    }
}