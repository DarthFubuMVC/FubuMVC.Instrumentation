using FubuMVC.Core;
using FubuMVC.Instrumentation.Sample.Controllers;
using FubuMVC.Instrumentation.Sample.Conventions;

namespace FubuMVC.Instrumentation.Sample
{
    public class SampleRegistery : FubuRegistry
    {
        public SampleRegistery()
        {


            Actions.FindBy(a =>
            {
                a.Applies.ToThisAssembly();
                a.IncludeClassesSuffixedWithController();
            });

            Routes
                .HomeIs<HomeInputModel>()
                .IgnoreNamespaceText("Controllers")
                .IgnoreNamespaceText("instrumentation")
                .IgnoreNamespaceText("fubumvc")
                .IgnoreMethodSuffix("Index");

            Policies.Add<OtherConvention>();
        }
    }
}