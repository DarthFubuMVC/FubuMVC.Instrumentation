using System;
using System.Linq;
using FubuMVC.Core.Behaviors;
using FubuMVC.Core.Registration;
using FubuMVC.Core.Registration.Nodes;
using FubuMVC.Core.Registration.ObjectGraph;
using FubuMVC.Diagnostics.Runtime;
using FubuMVC.Instrumentation.Chains;
using FubuTestingSupport;
using NUnit.Framework;
using SampleNode;

namespace FubuMVC.Instrumentation.Tests.Chains
{
    [TestFixture]
    public class AverageChainVisualizerBuilderTester : InteractionContext<AverageChainVisualizerBuilder>
    {
        private BehaviorGraph _graph;

        protected override void  beforeEach()
        {
            _graph = new BehaviorGraph();

            Services.Inject(_graph);
        }

        [Test]
        public void Handles_request_for_expired_routes()
        {
            var averageModel = ClassUnderTest.VisualizerFor(Guid.Empty);
            averageModel.ShouldBeNull();
        }

        [Test]
        public void Does_not_vizualize_diagnostics_behaviors()
        {
            var chain = new BehaviorChain();

            new DiagnosticNode(chain);

            _graph.AddChain(chain);

            var averageModel = ClassUnderTest.VisualizerFor(chain.UniqueId);

            averageModel.BehaviorAverages.Count().ShouldEqual(0);

        }

        [Test]
        public void Visualizes_behaviors()
        {
            var chain = new BehaviorChain();

            chain.AddToEnd<SpecialTestNode>();
            _graph.AddChain(chain);

            var averageModel = ClassUnderTest.VisualizerFor(chain.UniqueId);

            averageModel.BehaviorAverages.ShouldNotBeEmpty();

        }
    }
}

namespace SampleNode
{
    public class SpecialTestBehavior : BasicBehavior
    {
        public SpecialTestBehavior(PartialBehavior partialBehavior) : base(partialBehavior)
        {
        }
    }

    public class SpecialTestNode : BehaviorNode
    {
        protected override ObjectDef buildObjectDef()
        {
          return new ObjectDef(typeof (SpecialTestBehavior), new Type[0]);
        }

        public override BehaviorCategory Category
        {
            get { return BehaviorCategory.Wrapper; }
        }
    }
}