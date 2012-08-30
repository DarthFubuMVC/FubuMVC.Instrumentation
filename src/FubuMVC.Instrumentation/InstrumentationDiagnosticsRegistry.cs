using FubuMVC.Core;
using FubuMVC.Instrumentation.Features;
using FubuMVC.Instrumentation.Navigation;
using FubuMVC.Spark;

namespace FubuMVC.Instrumentation
{
    public class InstrumentationDiagnosticsRegistry : FubuRegistry
    {
        public InstrumentationDiagnosticsRegistry()
        {
            Applies
                .ToAssemblyContainingType<InstrumentationDiagnosticsRegistry>();

            ApplyHandlerConventions(markers => new InstrumentationHandlerUrlPolicy(markers),
                                    typeof(InstrumentationHandlers));

            //Import<InstrumentationHandlerUrlPolicy>(() => new InstrumentationHandlerUrlPolicy(markers));

            Views
                .TryToAttachWithDefaultConventions();

            //Routes
            //    .UrlPolicy<DiagnosticsAttributeUrlPolicy>();

            Navigation<InstrumentationMenu>();

            //Services(x =>
            //{
            //    x.SetServiceIfNone<IInstrumentationReportCache, InstrumentationReportCache>();
            //    x.SetServiceIfNone<IGridRowProvider<InstrumentationCacheModel, RouteInstrumentationModel>, InstrumentationCacheRowProvider>();
            //    x.SetServiceIfNone<IAverageChainVisualizerBuilder, AverageChainVisualizerBuilder>();
            //});

            Import<SparkEngine>();
        }
    }
}