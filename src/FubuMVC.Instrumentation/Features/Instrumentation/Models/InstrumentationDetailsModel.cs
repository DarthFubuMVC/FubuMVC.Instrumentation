using System.Collections.Generic;
using FubuMVC.Instrumentation.Diagnostics;

namespace FubuMVC.Instrumentation.Features.Instrumentation.Models
{
    public class InstrumentationDetailsModel : RouteInstrumentationModel
    {

        public InstrumentationDetailsModel()
        {
            RequestOverviews = new List<InstrumentationRequestOverviewModel>();
        }

        public InstrumentationDetailsModel(RouteInstrumentationReport report) : base(report)
        {
            RequestOverviews = new List<InstrumentationRequestOverviewModel>();
        }

        public IList<InstrumentationRequestOverviewModel> RequestOverviews { get; set; }
        public AverageChainModel AverageChain { get; set; }
    }
}
