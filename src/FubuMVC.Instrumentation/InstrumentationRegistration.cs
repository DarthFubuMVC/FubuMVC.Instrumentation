using FubuMVC.Core;
using FubuMVC.Diagnostics;
using FubuMVC.Instrumentation.Runtime;

namespace FubuMVC.Instrumentation
{
    public class InstrumentationRegistration : IFubuRegistryExtension
    {
        public void Configure(FubuRegistry registry)
        {
            registry.Import<InstrumentationDiagnosticsRegistry>(DiagnosticsRegistration.DIAGNOSTICS_URL_ROOT);

            // Nothing, but do this to force it to create InstrumentationSettings
            // now, because it creates a bi-directional dependency later
            registry.AlterSettings<InstrumentationSettings>(x => {});
        }
    }
}