using System;
using System.Diagnostics;
using FubuMVC.Core.Http;
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
        public InstrumentationRequestLog Current { get; set; }

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
            Current = new InstrumentationRequestLog
            {
                RequestLog = _builder.BuildForCurrentRequest()
            };
        }

        public void MarkFinished()
        {
            _stopwatch.Stop();

            Current.RequestLog.ExecutionTime = _stopwatch.ElapsedMilliseconds;
            try
            {
                Current.RequestLog.ResponseHeaders = _response.AllHeaders();
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
            Current.RequestLog.AddLog(_stopwatch.ElapsedMilliseconds, message);
        }

        public void MarkAsFailedRequest(Exception ex)
        {
            Current.RequestLog.Failed = true;
            Current.Exception = ex;
        }

        public string LogUrl
        {
            get { return Current.RequestLog.ReportUrl; }
        }
    }

    public interface IInstrumentationRequestTrace
    {
        string LogUrl { get; }

        void Start();

        void MarkFinished();

        void Log(object message);

        void MarkAsFailedRequest(Exception ex);
    }
}