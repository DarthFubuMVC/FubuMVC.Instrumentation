using System;
using System.Linq;
using FubuMVC.Core.Registration;
using FubuMVC.Core.Registration.Nodes;
using FubuMVC.Core.Registration.Routes;
using FubuMVC.Diagnostics.Runtime;
using FubuMVC.Instrumentation.Runtime;
using FubuTestingSupport;
using NUnit.Framework;

namespace FubuMVC.Instrumentation.Tests.Runtime
{
    [TestFixture]
    public class InstrumentationReportCacheTester: InteractionContext<InstrumentationReportCache>
    {
        private BehaviorGraph _graph;

        protected override void beforeEach()
        {
            _graph = new BehaviorGraph();
            Services.Inject(_graph);
            _graph = Container.GetInstance<BehaviorGraph>();
        }

        [Test]
        public void Should_store_and_retrieve_log()
        {
            var chain = new BehaviorChain
            {
                Route = new RouteDefinition("some/pattern")
            };

            _graph.AddChain(chain);

            var log = new RequestLog
            {
                ChainId = chain.UniqueId
            };

            ClassUnderTest.Store(log);

            var report = ClassUnderTest.GetReport(log.ChainId);

            Assert.IsNotNull(report);
            Assert.IsNotNull(report.Reports);

            report.Reports.First().Id.ShouldEqual(log.Id);
        }

        [Test]
        public void Should_not_find_report_for_non_existing_chain()
        {
            var chain = new BehaviorChain
            {
                Route = new RouteDefinition("some/pattern")
            };

            _graph.AddChain(chain);

            ClassUnderTest.Store(new RequestLog
            {
                ChainId = chain.UniqueId
            });

            var report = ClassUnderTest.GetReport(Guid.NewGuid());

            Assert.IsNull(report);
        }
    }
}