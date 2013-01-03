using FubuMVC.Core;
using FubuMVC.Instrumentation.Features.Instrumentation.Models;

namespace FubuMVC.Instrumentation.Features.Instrumentation
{
    public class InstrumentationEndpoint
    {
        [UrlPattern("instrumentation")]
        public InstrumentationCacheModel get_instrumentation_details(InstrumentationRequestModel inputModel)
        {
            return new InstrumentationCacheModel();
        }
    }
}