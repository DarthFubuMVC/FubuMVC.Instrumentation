using FubuMVC.Core;
using FubuMVC.Diagnostics;

namespace FubuMVC.Instrumentation
{
    public class InstrumentationRegistration : IFubuRegistryExtension
    {
        public void Configure(FubuRegistry registry)
        {
            registry.Import<InstrumentationDiagnosticsRegistry>(DiagnosticsRegistration.DIAGNOSTICS_URL_ROOT);
        }
    }
}