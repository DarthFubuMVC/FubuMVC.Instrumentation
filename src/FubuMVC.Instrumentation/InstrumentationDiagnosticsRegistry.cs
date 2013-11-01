using FubuMVC.Core;
using FubuMVC.Diagnostics.Runtime;
using FubuMVC.Instrumentation.Chains;
using FubuMVC.Instrumentation.Runtime;

namespace FubuMVC.Instrumentation
{
    public class InstrumentationDiagnosticsRegistry : FubuRegistry
    {
        public InstrumentationDiagnosticsRegistry()
        {
            Actions.FindBy(a =>
            {
                a.Applies.ToThisAssembly();
                a.IncludeClassesSuffixedWithEndpoint();
            });

            Services(x =>
            {
                x.SetServiceIfNone<IInstrumentationReportCache,InstrumentationReportCache>();
                x.SetServiceIfNone<IAverageChainVisualizerBuilder, AverageChainVisualizerBuilder>();
                x.AddService<IRequestTraceObserver, InstrumentationRequestNotifier>();
            });
        }
    }
}