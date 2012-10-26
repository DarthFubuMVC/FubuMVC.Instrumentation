using FubuMVC.Core.Registration.Nodes;
using FubuMVC.Core.Registration.Routes;
using FubuMVC.Instrumentation.Runtime;
using NUnit.Framework;

namespace FubuMVC.Instrumentation.Tests.Runtime
{
    [TestFixture]
    public class BehaviorExtensionsTester
    {
        private const string Na = "N/A";

        [Test]
        public void Should_get_na_for_route_when_chain_is_null()
        {
            BehaviorChain nullChain = null;
            Assert.AreEqual(Na, nullChain.GetRoute());
        }

        [Test]
        public void Should_get_na_for_route_when_chain_route_is_null()
        {
            var chainWithoutRoute = new BehaviorChain
            {
                Route = null
            };

            Assert.AreEqual(Na, chainWithoutRoute.GetRoute());
        }


        [Test]
        public void Should_get_na_for_route_when_chain_route_pattern_is_null()
        {
            var chainWithoutRoute = new BehaviorChain
            {
                Route = new RouteDefinition(null)
            };

            Assert.AreEqual(Na, chainWithoutRoute.GetRoute());
        }


        [Test]
        public void Should_get_partial_for_route_when_chain_is_partial_only()
        {
            var chainWithoutRoute = new BehaviorChain
            {
                Route = new RouteDefinition("~/"),
                IsPartialOnly = true
            };

            Assert.AreEqual("(partial)", chainWithoutRoute.GetRoute());
        }

        [Test]
        public void Should_get_default_for_route_when_chain_route_pattern_is_empty()
        {
            var chainWithoutRoute = new BehaviorChain
            {
                Route = new RouteDefinition(string.Empty),
            };

            Assert.AreEqual("(default)", chainWithoutRoute.GetRoute());
        }

        [Test]
        public void Should_get_actual_route_when_chain_route_pattern_is_a_regular_route()
        {
            const string pattern = "hello/world";

            var chainWithoutRoute = new BehaviorChain
            {
                Route = new RouteDefinition(pattern),
            };

            Assert.AreEqual(pattern, chainWithoutRoute.GetRoute());
        }
    }
}