using System;

namespace FubuMVC.Instrumentation.Features.Instrumentation.Models
{
    public class AverageBehaviorModel
    {
        public Guid Id { get; set; }
        public string BehaviorType { get; set; }

        public int HitCount { get; set; }

        public double AverageExecutionTime
        {
            get 
            {
                var value = TotalExecutionTime / HitCount;
                return Double.IsNaN(value) ? 0 : value; 
            }
        }

        public double TotalExecutionTime { get; set; }
    }
}