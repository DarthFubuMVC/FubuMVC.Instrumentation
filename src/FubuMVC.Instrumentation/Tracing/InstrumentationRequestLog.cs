using System;
using FubuMVC.Diagnostics.Runtime;

namespace FubuMVC.Instrumentation.Tracing
{
    public class InstrumentationRequestLog
    {
        public RequestLog RequestLog { get; set; }
        public Exception Exception { get; set; }
    }
}