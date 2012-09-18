using System;
using FubuMVC.Instrumentation.Runtime;

namespace FubuMVC.Instrumentation.Features.Instrumentation.Models
{
    public class RouteInstrumentationModel
    {
        public Guid Id { get; set; }
        public string Url { get; set; }
        public string ReportUrl { get; set; }
        public long HitCount { get; set; }
        public decimal AverageExecution { get; set; }
        public long MinExecution { get; set; }
        public long MaxExecution { get; set; }
        public long ExceptionCount { get; set; }

        public string ExceptionFrequency
        {
            get { return String.Format("{0}%", (int)((ExceptionCount / (double)HitCount) * 100)); }
        }

        public RouteInstrumentationModel()
        {

        }

        public RouteInstrumentationModel(RouteInstrumentationReport report)
        {
            Id = report.Id;
            Url = report.Url;
            ReportUrl = report.ReportUrl;
            HitCount = report.HitCount;
            AverageExecution = report.AverageExecution;
            MinExecution = report.MinExecution;
            MaxExecution = report.MaxExecution;
            ExceptionCount = report.ExceptionCount;
        }
    }
}