namespace FubuMVC.Instrumentation.Sample.Controllers
{
    public class HomeController
    {
         public HomeViewModel Index(HomeInputModel inputModel)
         {
             return new HomeViewModel();
         }
    }
}