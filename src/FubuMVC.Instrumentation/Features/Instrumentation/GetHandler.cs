using FubuMVC.Instrumentation.Features.Instrumentation.Models;

namespace FubuMVC.Instrumentation.Features.Instrumentation
{
    public class GetHandler
    {
        public InstrumentationCacheModel Execute(InstrumentationRequestModel inputModel)
        {
            return new InstrumentationCacheModel();
        }
    }
}