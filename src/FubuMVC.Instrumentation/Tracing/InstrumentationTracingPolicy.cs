using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FubuCore;
using FubuMVC.Core.Registration;
using FubuMVC.Core.Registration.Nodes;
using FubuMVC.Diagnostics.Runtime.Tracing;

namespace FubuMVC.Instrumentation.Tracing
{
    public class InstrumentationTracingPolicy : IConfigurationAction
    {
        private readonly Assembly _assmbly;

        public InstrumentationTracingPolicy()
        {
            _assmbly = GetType().Assembly;
        }

        public void Configure(BehaviorGraph graph)
        {
            graph.Behaviors.Where(ShouldApply).Each(x => new InstrumentationNode(x));
        }

        private bool ShouldApply(BehaviorChain chain)
        {
            var route = chain.GetRoutePattern() ?? string.Empty;

            if(route.Contains(DiagnosticUrlPolicy.DIAGNOSTICS_URL_ROOT))
            {
                return false;
            }

            if(chain.HasResourceType() && PartOfDiagnostics(chain.ResourceType()))
            {
                return false;
            }

            if(chain.InputType() != null && PartOfDiagnostics(chain.InputType()))
            {
                return false;
            }

            return true;
        }

        private bool PartOfDiagnostics(Type type)
        {
            var diagnosticsAssembly = typeof (DiagnosticUrlPolicy).Assembly;
            if(type.IsGenericEnumerable())
            {
                return type.GetGenericArguments().Any(x => x.Assembly == type.Assembly || x.Assembly == diagnosticsAssembly);
            }
            return type.Assembly == _assmbly || type.Assembly == diagnosticsAssembly;
        }
    }
}