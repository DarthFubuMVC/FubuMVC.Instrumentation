using FubuMVC.Core.Registration.Nodes;

namespace FubuMVC.Instrumentation.Runtime
{
    public static class BehaviorExtensions
    {
        public static string GetRoute(this BehaviorChain chain)
        {
            if (chain == null)
            {
                return "N/A";
            }

            if (chain.IsPartialOnly)
            {
                return "(partial)";
            }

            var routed = chain as RoutedChain;

            if (routed == null)
            {
                return "N/A";
            }

            var pattern = routed.GetRoutePattern();
            if (pattern == string.Empty)
            {
                pattern = "(default)";
            }

            return pattern;
        }
    }
}