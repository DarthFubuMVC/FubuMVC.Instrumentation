using FubuMVC.Core.UI.Navigation;
using FubuMVC.Diagnostics.Navigation;
using FubuMVC.Instrumentation.Features.Instrumentation.Models;

namespace FubuMVC.Instrumentation.Navigation
{
    public class InstrumentationMenu : NavigationRegistry
    {
        public InstrumentationMenu()
        {
            ForMenu(DiagnosticKeys.Main);
            Add += MenuNode.ForInput<InstrumentationRequestModel>(InstrumentationKeys.Routes);
        }
    }
}