using System.Collections.Generic;
using System.Linq;
using FubuMVC.Core.Registration;
using FubuMVC.Diagnostics.Runtime.Tracing;

namespace FubuMVC.Instrumentation.Tracing
{
    public class InstrumentationTracingPolicy : IConfigurationAction
    {
        public void Configure(BehaviorGraph graph)
        {
            graph.Behaviors
                .Where(x => !string.IsNullOrEmpty(x.GetRoutePattern()) &&
                    !x.GetRoutePattern().Contains(DiagnosticUrlPolicy.DIAGNOSTICS_URL_ROOT))
                .Each(x => new InstrumentationNode(x));
        }
    }
}