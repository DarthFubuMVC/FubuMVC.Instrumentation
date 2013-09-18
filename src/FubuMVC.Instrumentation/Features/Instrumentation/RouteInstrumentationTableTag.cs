using System.Collections.Generic;
using FubuMVC.Instrumentation.Features.Instrumentation.Models;
using HtmlTags;

namespace FubuMVC.Instrumentation.Features.Instrumentation
{
    public class RouteInstrumentationTableTag : TableTag
    {
        public RouteInstrumentationTableTag(IEnumerable<RouteInstrumentationModel> models)
        {
            AddClass("table");
            AddClass("table-bordered");

            AddHeaderRow(row => {
                row.Header("Route");
                row.Header("Average Execution Time (ms)");
                row.Header("Hit Count");
                row.Header("Exception Count");
                row.Header("Exception Frequency");
                row.Header("Min Execution Time (ms)");
                row.Header("Max Execution Time (ms)");
            });

            models.Each(writeModel);
        }

        private void writeModel(RouteInstrumentationModel model)
        {
            AddBodyRow(row => {
                row.AddClass("instrumentation-row");
                row.Data("id", model.Id);
                row.Data("url", model.ReportUrl);

                row.Cell(model.Url);
                row.Cell(model.AverageExecution);
                row.Cell(model.HitCount);
                row.Cell(model.ExceptionCount);
                row.Cell(model.ExceptionFrequency).Style("text-align", "right");
                row.Cell(model.MinExecution);
                row.Cell(model.MaxExecution);

            });
        }
    }
}