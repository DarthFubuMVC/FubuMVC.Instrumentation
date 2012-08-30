using FubuMVC.Instrumentation.Features.Routes.Models;

namespace FubuMVC.Instrumentation.Features.Routes
{
    public class GetHandler
    {
        public InstrumentationCacheModel Execute(InstrumentationRequestModel inputModel)
        {
            return new InstrumentationCacheModel();
        }
    }
}