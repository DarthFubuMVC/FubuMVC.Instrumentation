using System.Collections.Generic;
using System.Linq;
using FubuMVC.Core.Registration;
using FubuMVC.Instrumentation.Sample.Behaviors;

namespace FubuMVC.Instrumentation.Sample.Conventions
{
    public class OtherConvention : IConfigurationAction
    {
        public void Configure(BehaviorGraph graph)
        {
            graph.Actions()
                .Where(x => x.HasInput && x.InputType()
                           .GetProperties()
                           .Any(p => p.Name.Contains("HelloText")))
                .Each(x => x.WrapWith<HelloTextBehavior>());
        }
    }
}