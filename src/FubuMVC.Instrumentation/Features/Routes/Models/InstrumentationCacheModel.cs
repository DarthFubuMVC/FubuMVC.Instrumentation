using System.Collections.Generic;

namespace FubuMVC.Instrumentation.Features.Routes.Models
{
    public class InstrumentationCacheModel //: IGridModel
    {
        //public JqGridColumnModel ColumnModel { get; set; }
        //public JsonGridFilter Filter { get; set; }
        public List<RouteInstrumentationModel> RouteInstrumentations { get; set; }
    }
}