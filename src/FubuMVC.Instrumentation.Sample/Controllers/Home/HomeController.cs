using System;
using System.Threading;
using FubuMVC.Core.Continuations;

namespace FubuMVC.Instrumentation.Sample.Controllers
{
    public class HomeController
    {
        public HomeViewModel Index(HomeInputModel inputModel)
        {
            return new HomeViewModel();
        }

        public string OtherView(OtherInputModel inputModel)
        {
            return inputModel.HelloText;
        }

        public FubuContinuation ReallyLongRequest()
        {
            Thread.Sleep(10000);
            return FubuContinuation.RedirectTo("/_fubu/instrumentation");
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