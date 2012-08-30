using FubuMVC.Diagnostics.Runtime;
using FubuMVC.Diagnostics.Runtime.Tracing;
using FubuMVC.Instrumentation.Diagnostics;

namespace FubuMVC.Instrumentation.Features.Instrumentation
{
    public class InstrumentationRequestTrace : IRequestTrace
    {
        private readonly IInstrumentationReportCache _cache;
        private readonly IRequestLogBuilder _builder;
        public RequestLog Current { get; set; }

        public InstrumentationRequestTrace(IInstrumentationReportCache cache, IRequestLogBuilder builder)
        {
            _cache = cache;
            _builder = builder;
        }

        public void Start()
        {
            Current = _builder.BuildForCurrentRequest();
            _cache.Store(Current);
        }

        public void MarkFinished()
        {
            throw new System.NotImplementedException();
        }

        public void Log(object message)
        {
            throw new System.NotImplementedException();
        }

        public void MarkAsFailedRequest()
        {
            Current.Failed = true;
        }

        public string LogUrl
        {
            get { throw new System.NotImplementedException(); }
        }
    }
}