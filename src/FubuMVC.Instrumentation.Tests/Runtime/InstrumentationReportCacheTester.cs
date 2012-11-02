using System;
using System.Linq;
using FubuMVC.Core.Registration;
using FubuMVC.Core.Registration.Nodes;
using FubuMVC.Core.Registration.Routes;
using FubuMVC.Diagnostics.Runtime;
using FubuMVC.Instrumentation.Runtime;
using FubuTestingSupport;
using NUnit.Framework;
using SampleNode;

namespace FubuMVC.Instrumentation.Tests.Runtime
{
    [TestFixture]
    public class InstrumentationReportCacheTester : InteractionContext<InstrumentationReportCache>
    {
        private BehaviorGraph _graph;

        protected override void beforeEach()
        {
            _graph = new BehaviorGraph();
            Services.Inject(_graph);
        }

        [Test]
        public void Should_store_and_retrieve_log()
        {
            var chain = BuildChain();

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
            var chain = BuildChain();

            ClassUnderTest.Store(new RequestLog
            {
                ChainId = chain.UniqueId
            });

            var report = ClassUnderTest.GetReport(Guid.NewGuid());

            Assert.IsNull(report);
        }

        [Test]
        public void Should_get_all_reports_for_logged_chains()
        {
            var chain1 = BuildChain("chain/1");
            var chain2 = BuildChain("chain/2");

            ClassUnderTest.Store(new RequestLog { ChainId = chain1.UniqueId });
            ClassUnderTest.Store(new RequestLog { ChainId = chain2.UniqueId });

            ClassUnderTest.Count().ShouldEqual(2);
        }

        private BehaviorChain BuildChain(string pattern = "some/pattern")
        {
           var chain = new BehaviorChain
           {
               Route = new RouteDefinition(pattern)
           };
            chain.AddToEnd(new StubNode());

            _graph.AddChain(chain);

           return chain;
        }
    }
}