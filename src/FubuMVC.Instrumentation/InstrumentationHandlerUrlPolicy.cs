using System;
using FubuMVC.Core.Registration.Conventions;
using FubuMVC.Core.Registration.Routes;

namespace FubuMVC.Instrumentation
{
    public class InstrumentationHandlerUrlPolicy : HandlersUrlPolicy
    {
        public InstrumentationHandlerUrlPolicy(params Type[] markers)
            : base(markers)
        {
        }

        protected override void visit(IRouteDefinition routeDefinition)
        {
            base.visit(routeDefinition);
            routeDefinition.Append("/instrumentation");
        }
    }
}