using System;
using System.Linq;
using FubuMVC.Core.Behaviors;
using FubuMVC.Core.Diagnostics.Runtime;
using FubuMVC.Core.Diagnostics.Runtime.Tracing;
using FubuMVC.Core.Registration;
using FubuMVC.Core.Registration.Nodes;
using FubuMVC.Core.Registration.ObjectGraph;
using FubuMVC.Instrumentation.Chains;
using FubuMVC.Instrumentation.Runtime;
using FubuTestingSupport;
using NUnit.Framework;
using Rhino.Mocks;
using SampleNode;

namespace FubuMVC.Instrumentation.Tests.Chains
{
    [TestFixture]
    public class AverageChainVisualizerBuilderTester : InteractionContext<AverageChainVisualizerBuilder>
    {
        private BehaviorGraph _graph;

        protected override void beforeEach()
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

            averageModel.BehaviorAverages.ToList().ShouldBeEmpty();
        }

        [Test]
        public void Does_not_vizualize_instrumentation_behaviors()
        {
            var chain = new BehaviorChain();

            chain.AddToEnd<InstrumentationNode>();

            _graph.AddChain(chain);

            var averageModel = ClassUnderTest.VisualizerFor(chain.UniqueId);

            averageModel.BehaviorAverages.ToList().ShouldBeEmpty();
        }

        [Test]
        public void Visualizes_behaviors()
        {
            var chain = new BehaviorChain();

            chain.AddToEnd<StubNode>();
            _graph.AddChain(chain);

            var averageModel = ClassUnderTest.VisualizerFor(chain.UniqueId);

            averageModel.BehaviorAverages.ShouldNotBeEmpty();
        }

        [Test]
        public void Visualizes_behaviors_in_correct_order()
        {
            var chain = new BehaviorChain();

            chain.AddToEnd<StubNode>();
            chain.AddToEnd<AnotherStubNode>();
            _graph.AddChain(chain);

            var averageModel = ClassUnderTest.VisualizerFor(chain.UniqueId);

            var averages = averageModel.BehaviorAverages.ToList();

            averages[0].BehaviorType.ShouldEqual("StubNode");
            averages[1].BehaviorType.ShouldEqual("AnotherStubNode");
        }

        [Test]
        public void Visualizes_behaviors_with_correct_averages()
        {
            var chain = new BehaviorChain();
            var node = new StubNode();
            var runningTimeSeconds = 10;
            chain.AddToEnd(node);

            var report = new RouteInstrumentationReport(new InstrumentationSettings(), chain.UniqueId);
            report.AddReportLog(BuildLog(node, runningTimeSeconds));

            MockFor<IInstrumentationReportCache>()
                .Stub(x => x.GetReport(chain.UniqueId))
                .Return(report);

            _graph.AddChain(chain);

            var averageModel = ClassUnderTest.VisualizerFor(chain.UniqueId);

            var averages = averageModel.BehaviorAverages;

            averages.ShouldNotBeEmpty();
            averages.First().TotalExecutionTime.ShouldEqual(runningTimeSeconds*1000);
        }

        private RequestLog BuildLog(BehaviorNode node, int runningTimeSeconds = 10)
        {
            var time = LocalSystemTime;
            var log = new RequestLog
            {
                ChainId = node.UniqueId
            };
            var correlation = new BehaviorCorrelation(node);


            log.AddLog(0, new BehaviorStart(correlation)
            {
                Time = time
            });

            log.AddLog(0, new BehaviorFinish(correlation)
            {
                Time = time.AddSeconds(runningTimeSeconds)
            });

            return log;
        }

        private class InstrumentationNode : BehaviorNode
        {
            protected override ObjectDef buildObjectDef()
            {
                return new ObjectDef(typeof (DiagnosticBehavior), new Type[0]);
            }

            public override BehaviorCategory Category
            {
                get { return BehaviorCategory.Instrumentation; }
            }
        }
    }
}

namespace SampleNode
{
    public class StubBehavior : BasicBehavior
    {
        public StubBehavior(PartialBehavior partialBehavior) : base(partialBehavior)
        {
        }
    }

    public class StubNode : BehaviorNode
    {
        protected override ObjectDef buildObjectDef()
        {
            return new ObjectDef(typeof (StubBehavior), new Type[0]);
        }

        public override BehaviorCategory Category
        {
            get { return BehaviorCategory.Wrapper; }
        }
    }

    public class AnotherStubNode : BehaviorNode
    {
        protected override ObjectDef buildObjectDef()
        {
            return new ObjectDef(typeof (StubBehavior), new Type[0]);
        }

        public override BehaviorCategory Category
        {
            get { return BehaviorCategory.Wrapper; }
        }
    }
}