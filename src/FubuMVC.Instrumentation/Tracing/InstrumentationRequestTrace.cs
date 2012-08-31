using System;
using System.Diagnostics;
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
        private readonly Stopwatch _stopwatch = new Stopwatch();
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
            _stopwatch.Start();
            Current = _builder.BuildForCurrentRequest();
        }

        public void MarkFinished()
        {
            _stopwatch.Stop();

            Current.ExecutionTime = _stopwatch.ElapsedMilliseconds;
            try
            {
                Current.ResponseHeaders = _response.AllHeaders();
            }
            catch (Exception ex)
            {
                //TODO: log iis errors.
            }
            finally
            {
                _cache.Store(Current);
            }
        }

        public void Log(object message)
        {
            Current.AddLog(_stopwatch.ElapsedMilliseconds, message);
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