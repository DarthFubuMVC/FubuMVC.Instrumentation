using FubuMVC.Instrumentation.Features.Instrumentation.Models;
using FubuMVC.SlickGrid;

namespace FubuMVC.Instrumentation.Features.Instrumentation
{
    public class RoutesGrid : GridDefinition<RouteInstrumentationModel>
    {
        public RoutesGrid()
        {
            SourceIs<RoutesSource>();
            Column(x => x.Url).Title("Route");
            Column(x => x.AverageExecution).Title("Average Execution Time (ms)");
            Column(x => x.HitCount).Title("Hit Count");
            Column(x => x.ExceptionCount).Title("Exception Count");
            Column(x => x.ExceptionFrequency).Title("Exception Frequency");
            Column(x => x.MinExecution).Title("Min Execution Time (ms)");
            Column(x => x.MaxExecution).Title("Max Execution Time (ms)");
            Data(x => x.Id);
            Data(x => x.ReportUrl);
        }
    }
}