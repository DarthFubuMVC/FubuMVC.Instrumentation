using FubuMVC.Diagnostics.Runtime;

namespace FubuMVC.Instrumentation.Features.Instrumentation.Models
{
    public class InstrumentationRequestDetailsModel
    {
        public InstrumentationRequestDetailsModel(RequestLog log)
        {
            Log = log;
        }
        public RequestLog Log { get; set; }
    }
}