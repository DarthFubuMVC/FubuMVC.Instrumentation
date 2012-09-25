using FubuMVC.Core.Registration.Nodes;

namespace FubuMVC.Instrumentation.Runtime
{
    public static class BehaviorExtensions
    {
        public static string GetRoute(this BehaviorChain chain)
        {
                if (chain == null || chain.Route == null || chain.Route.Pattern == null)
                {
                    return "N/A";
                }

                if (chain.IsPartialOnly)
                {
                    return "(partial)";
                }

                var pattern = chain.Route.Pattern;
                if (pattern == string.Empty)
                {
                    pattern = "(default)";
                }

                return pattern;
        }
    }
}