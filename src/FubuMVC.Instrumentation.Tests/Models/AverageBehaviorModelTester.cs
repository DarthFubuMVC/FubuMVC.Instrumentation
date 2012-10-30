using FubuMVC.Instrumentation.Features.Instrumentation.Models;
using FubuTestingSupport;
using NUnit.Framework;

namespace FubuMVC.Instrumentation.Tests.Models
{
    [TestFixture]
    public class AverageBehaviorModelTester : InteractionContext<AverageBehaviorModel>
    {
        [Test]
        public void Should_get_average_execution_time_based_on_actual_total_execution_time_and_hit_count()
        {
            ClassUnderTest.HitCount = 10;
            ClassUnderTest.ActualTotalExecutionTime = 10;

            Assert.AreEqual(1, ClassUnderTest.AverageExecutionTime);
        }
    }
}