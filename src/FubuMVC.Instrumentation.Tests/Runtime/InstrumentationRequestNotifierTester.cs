using FubuMVC.Core.Diagnostics.Runtime;
using FubuMVC.Instrumentation.Runtime;
using FubuTestingSupport;
using NUnit.Framework;
using Rhino.Mocks;

namespace FubuMVC.Instrumentation.Tests.Runtime
{
    public class InstrumentationRequestNotifierTester : InteractionContext<InstrumentationRequestNotifier>
    {
        private RequestLog _log;

        protected override void beforeEach()
        {
            _log = new RequestLog();
        }

        [Test]
        public void Should_store_report_on_completed()
        {
            ClassUnderTest.Completed(_log);

            MockFor<IInstrumentationReportCache>()
                .AssertWasCalled(x => x.Store(_log));
        }

        [Test]
        public void Should_not_store_report_on_started()
        {
            ClassUnderTest.Started(_log);

            MockFor<IInstrumentationReportCache>()
                .AssertWasNotCalled(x => x.Store(_log));
        }
    }
}