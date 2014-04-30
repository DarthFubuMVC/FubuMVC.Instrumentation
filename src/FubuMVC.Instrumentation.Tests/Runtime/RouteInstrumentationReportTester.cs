using System;
using System.Linq;
using FubuMVC.Core.Diagnostics.Runtime;
using FubuMVC.Instrumentation.Runtime;
using FubuTestingSupport;
using NUnit.Framework;

namespace FubuMVC.Instrumentation.Tests.Runtime
{
    [TestFixture]
    public class RouteInstrumentationReportTester : InteractionContext<RouteInstrumentationReport>
    {
        private InstrumentationSettings _settings;

        protected override void beforeEach()
        {
            _settings = new InstrumentationSettings
            {
                MaxRequestsPerRoute = 2,
                MaxErrorsPerRoute = 5
            };

            Services.Inject(new RouteInstrumentationReport(_settings, Guid.Empty, string.Empty ));
        }

        [Test]
        public void Should_set_max_execution_time_for_report()
        {
            Assert.AreEqual(0, ClassUnderTest.MaxExecution);

            var log = new RequestLog
            {
                ExecutionTime = 10
            };

            ClassUnderTest.AddReportLog(log);

            Assert.AreEqual(log.ExecutionTime, ClassUnderTest.MaxExecution);
        }

        [Test]
        public void Should_set_min_execution_time_for_report()
        {
            Assert.AreEqual(long.MaxValue, ClassUnderTest.MinExecution);

            var log = new RequestLog
            {
                ExecutionTime = 10
            };

            ClassUnderTest.AddReportLog(log);

            Assert.AreEqual(log.ExecutionTime, ClassUnderTest.MinExecution);
        }

        [Test]
        public void Should_calculate_average_execution_time_for_report()
        {
            Assert.AreEqual(long.MaxValue, ClassUnderTest.MinExecution);

            var log = new RequestLog
            {
                ExecutionTime = 10
            };

            ClassUnderTest.AddReportLog(log);

            var log2 = new RequestLog
            {
                ExecutionTime = 0
            };

            ClassUnderTest.AddReportLog(log2);

            Assert.AreEqual(5, ClassUnderTest.AverageExecution);
        }

        [Test]
        public void Should_increment_hit_count()
        {
            Assert.AreEqual(0, ClassUnderTest.Reports.Count);

            ClassUnderTest.AddReportLog(new RequestLog());

            Assert.AreEqual(1, ClassUnderTest.HitCount);
        }

        [Test]
        public void Should_increment_hit_count_for_failed_exception()
        {
            Assert.AreEqual(0, ClassUnderTest.Reports.Count);

            ClassUnderTest.AddReportLog(new RequestLog{ Failed = true});

            Assert.AreEqual(1, ClassUnderTest.HitCount);
        }

        [Test]
        public void Should_increment_exception_count_for_failed_request_log()
        {
            Assert.AreEqual(0, ClassUnderTest.ExceptionCount);

            ClassUnderTest.AddReportLog(new RequestLog{ Failed = true });

            Assert.AreEqual(1, ClassUnderTest.ExceptionCount);
        }

        [Test]
        public void Should_increment_hit_count_past_max_request()
        {
            Assert.AreEqual(0, ClassUnderTest.Reports.Count);

            var overMaxRequestsPerRoute = _settings.MaxRequestsPerRoute + 1;

            for (var i = 0; i < overMaxRequestsPerRoute; i++)
            {
                ClassUnderTest.AddReportLog(new RequestLog());
            }

            Assert.AreEqual(overMaxRequestsPerRoute, ClassUnderTest.HitCount);
            Assert.Greater(ClassUnderTest.HitCount, ClassUnderTest.Reports.Count);
        }

        [Test]
        public void Should_increment_exception_count_past_max_errors_per_route()
        {
            Assert.AreEqual(0, ClassUnderTest.ExceptionCount);

            var overMaxRequestsAndExceptionsPerRoute = _settings.MaxErrorsPerRoute
                + _settings.MaxRequestsPerRoute
                + 1;

            for (var i = 0; i < overMaxRequestsAndExceptionsPerRoute; i++)
            {
                ClassUnderTest.AddReportLog(new RequestLog{ Failed = true });
            }

            Assert.AreEqual(overMaxRequestsAndExceptionsPerRoute, ClassUnderTest.ExceptionCount);
            Assert.Greater(ClassUnderTest.ExceptionCount + ClassUnderTest.MaxStoredRequests, ClassUnderTest.Reports.Count);
        }

        [Test]
        public void Should_store_only_specified_amount_of_report_logs_for_a_given_report()
        {
            var overMaxRequestsPerRoute = _settings.MaxRequestsPerRoute + 1;

            for (var i = 0; i < overMaxRequestsPerRoute; i++)
            {
                ClassUnderTest.AddReportLog(new RequestLog());
            }

            Assert.AreEqual(_settings.MaxRequestsPerRoute, ClassUnderTest.Reports.Count);
        }

        [Test]
        public void Should_store_additional_exception_logs_after_report_logs_are_full_up_to_additional_limit()
        {
            var overMaxRequestsAndExceptionsPerRoute = _settings.MaxErrorsPerRoute
                + _settings.MaxRequestsPerRoute
                + 1;

            for (var i = 0; i < overMaxRequestsAndExceptionsPerRoute; i++)
            {
                ClassUnderTest.AddReportLog(new RequestLog
                {
                    Failed = true
                });
            }

            Assert.AreEqual(_settings.MaxErrorsPerRoute + _settings.MaxRequestsPerRoute,
                ClassUnderTest.Reports.Count(x => x.Failed));
        }
    }
}