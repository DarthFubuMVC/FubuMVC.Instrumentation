using System;
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

        public OtherViewModel OccasionalError(OtherInputModel inputModel)
        {
            var rand = new Random();
            if (rand.Next(0, 100) < 10)
            {
                throw new Exception("Boom");
            }
            return new OtherViewModel
            {
                HelloText = inputModel.HelloText
            };
        }
    }
}