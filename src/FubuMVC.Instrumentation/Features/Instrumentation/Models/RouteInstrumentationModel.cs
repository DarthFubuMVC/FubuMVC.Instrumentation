using System;
using FubuMVC.Instrumentation.Diagnostics;

namespace FubuMVC.Instrumentation.Features.Instrumentation.Models
{
    public class RouteInstrumentationModel
    {
        public Guid Id { get; set; }
        public string Url { get; set; }
        public long HitCount { get; set; }
        public decimal AverageExecution { get; set; }
        public long MinExecution { get; set; }
        public long MaxExecution { get; set; }
        public long ExceptionCount { get; set; }

        public RouteInstrumentationModel()
        {

        }

        public RouteInstrumentationModel(RouteInstrumentationReport report)
        {
            Id = report.Id;
            Url = report.Url;
            HitCount = report.HitCount;
            AverageExecution = report.AverageExecution;
            MinExecution = report.MinExecution;
            MaxExecution = report.MaxExecution;
            ExceptionCount = report.ExceptionCount;
        }
    }
}