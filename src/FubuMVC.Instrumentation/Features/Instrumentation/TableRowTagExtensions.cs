using HtmlTags;

namespace FubuMVC.Instrumentation.Features.Instrumentation
{
    public static class TableRowTagExtensions
    {
        public static HtmlTag Cell(this TableRowTag row, decimal number)
        {
            return row.Cell(number.ToString()).Style("text-align", "right");
        }
    }
}