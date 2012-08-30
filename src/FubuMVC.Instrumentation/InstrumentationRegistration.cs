using FubuMVC.Core;
using FubuMVC.Diagnostics.Runtime.Tracing;

namespace FubuMVC.Instrumentation
{
    public class InstrumentationRegistration : IFubuRegistryExtension
    {
        public void Configure(FubuRegistry registry)
        {
            registry.Import<InstrumentationDiagnosticsRegistry>(DiagnosticUrlPolicy.DIAGNOSTICS_URL_ROOT);
        }
    }
}