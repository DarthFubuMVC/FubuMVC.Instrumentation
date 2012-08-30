using System.Collections.Generic;

namespace FubuMVC.Instrumentation.Features.Routes.Models
{
    public class InstrumentationDetailsModel : RouteInstrumentationModel
    {
        public InstrumentationDetailsModel()
        {
            RequestOverviews = new List<InstrumentationRequestOverviewModel>();
        }

        public IList<InstrumentationRequestOverviewModel> RequestOverviews { get; set; }
        public AverageChainModel AverageChain { get; set; }
    }
}
