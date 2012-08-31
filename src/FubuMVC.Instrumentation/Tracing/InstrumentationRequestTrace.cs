using System;
using FubuMVC.Core.Http;
using FubuMVC.Diagnostics.Runtime;
using FubuMVC.Diagnostics.Runtime.Tracing;
using FubuMVC.Instrumentation.Diagnostics;

namespace FubuMVC.Instrumentation.Tracing
{
    public class InstrumentationRequestTrace : IInstrumentationRequestTrace
    {
        private readonly IInstrumentationReportCache _cache;
        private readonly IRequestLogBuilder _builder;
        private readonly IResponse _response;
        public RequestLog Current { get; set; }

        public InstrumentationRequestTrace(IInstrumentationReportCache cache,
            IRequestLogBuilder builder,
            IResponse response)
        {
            _cache = cache;
            _builder = builder;
            _response = response;
        }

        public void Start()
        {
            Current = _builder.BuildForCurrentRequest();
            _cache.Store(Current);
        }

        public void MarkFinished()
        {
            try
            {
                Current.ResponseHeaders = _response.AllHeaders();
            }
            catch (Exception ex)
            {
                //TODO: log iis errors.
            }
        }

        public void Log(object message)
        {
            throw new NotImplementedException();
        }

        public void MarkAsFailedRequest()
        {
            Current.Failed = true;
        }

        public string LogUrl
        {
            get { return Current.ReportUrl; }
        }
    }

    public interface IInstrumentationRequestTrace : IRequestTrace
    {
    }
}