using System.Linq;
using FubuMVC.Instrumentation.Features.Instrumentation.Models;
using FubuMVC.Instrumentation.Runtime;

namespace FubuMVC.Instrumentation.Features.Instrumentation.RequestDetails
{
    public class InstrumentationRequestDetailsEndpoint
    {
        private readonly IInstrumentationReportCache _reportCache;

        public InstrumentationRequestDetailsEndpoint(IInstrumentationReportCache reportCache)
        {
            _reportCache = reportCache;
        }

        public InstrumentationRequestDetailsModel get_instrumentation_request_details_Id_ReportId(InstrumentationRequestDetailsInputModel inputModel)
        {
            var report = _reportCache.GetReport(inputModel.Id);
            var log = report.Reports.FirstOrDefault(r => r.Id == inputModel.ReportId);

            return new InstrumentationRequestDetailsModel(log);
        }
    }
}