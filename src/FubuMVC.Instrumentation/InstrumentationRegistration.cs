using FubuMVC.Core;
using FubuMVC.Diagnostics.Runtime.Tracing;
using FubuMVC.Instrumentation.Tracing;

namespace FubuMVC.Instrumentation
{
    public class InstrumentationRegistration : IFubuRegistryExtension
    {
        public void Configure(FubuRegistry registry)
        {
            registry.Policies.Add<InstrumentationTracingPolicy>();
            registry.Import<InstrumentationDiagnosticsRegistry>(DiagnosticUrlPolicy.DIAGNOSTICS_URL_ROOT);
        }
    }
}