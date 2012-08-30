using FubuMVC.Core;
using FubuMVC.Instrumentation.Sample.Controllers;
using FubuMVC.Instrumentation.Sample.Conventions;
using FubuMVC.Spark;

namespace FubuMVC.Instrumentation.Sample
{
    public class SampleRegistery : FubuRegistry
    {
        public SampleRegistery()
        {
            Applies
                .ToThisAssembly();

            Import<SparkEngine>();
            Actions.IncludeClassesSuffixedWithController();
            Views.TryToAttachWithDefaultConventions();

            Routes
                .HomeIs<HomeInputModel>()
                .IgnoreNamespaceText("Controllers")
                .IgnoreNamespaceText("instrumentation")
                .IgnoreNamespaceText("fubumvc")
                .IgnoreMethodSuffix("Index");

            ApplyConvention<OtherConvention>();
        }
    }
}