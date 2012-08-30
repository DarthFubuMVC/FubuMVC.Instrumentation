using System;

namespace FubuMVC.Instrumentation.Sample.Controllers
{
    public class ErrorController
    {
         public ErrorViewModel Index()
         {
             throw new Exception("boom");
         }
    }
}