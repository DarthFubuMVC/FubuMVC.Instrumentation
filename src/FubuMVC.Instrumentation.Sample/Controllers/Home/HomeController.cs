using System.Threading;

namespace FubuMVC.Instrumentation.Sample.Controllers
{
    public class HomeController
    {
        public HomeViewModel Index(HomeInputModel inputModel)
        {
            return new HomeViewModel();
        }

        public OtherViewModel OtherView(OtherInputModel inputModel)
        {
            return new OtherViewModel
            {
                HelloText = inputModel.HelloText
            };
        }

        public ReallyLongRequestResourceModel ReallyLongRequest()
        {
            Thread.Sleep(10000);
            return new ReallyLongRequestResourceModel();
        }

    }
}