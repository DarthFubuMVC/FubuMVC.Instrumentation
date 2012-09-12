using System.Linq;
using FubuMVC.Instrumentation.Features.Instrumentation.Models;
using FubuMVC.Instrumentation.Runtime;

namespace FubuMVC.Instrumentation.Features.Instrumentation.RequestDetails
{
    public class GetHandler
    {
        private readonly IInstrumentationReportCache _reportCache;

        public GetHandler(IInstrumentationReportCache reportCache)
        {
            _reportCache = reportCache;
        }

        public InstrumentationRequestDetailsModel Execute(InstrumentationRequestDetailsInputModel inputModel)
        {
            var report = _reportCache.GetReport(inputModel.Id);
            var log = report.Reports.FirstOrDefault(r => r.Id == inputModel.ReportId);

            return new InstrumentationRequestDetailsModel(log);
        }
    }
}