using FubuMVC.Core.Registration.Nodes;
using FubuMVC.Core.Registration.ObjectGraph;

namespace FubuMVC.Instrumentation.Tracing
{
    public class InstrumentationNode : BehaviorNode
    {
        public InstrumentationNode(BehaviorChain chain)
        {
            chain.Prepend(this);
        }

        protected override ObjectDef buildObjectDef()
        {
            return new ObjectDef(typeof(InstrumentationTracingBehavior));
        }

        public override BehaviorCategory Category
        {
            get { return BehaviorCategory.Instrumentation; }
        }
    }
}