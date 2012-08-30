using System;

namespace FubuMVC.Instrumentation.Features.Routes.Models
{
    public class InstrumentationRequestOverviewModel
    {
        public Guid Id { get; set; }
        public string DateTime { get; set; }
        public string ExecutionTime { get; set; }
        public bool HasException { get; set; }
        public bool IsWarning { get; set; }
    }
}