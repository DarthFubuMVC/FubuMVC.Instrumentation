namespace FubuMVC.Instrumentation.Runtime
{
    public class InstrumentationSettings
    {
        public InstrumentationSettings()
        {
            MaxRequestsPerRoute = 50;
            MaxErrorsPerRoute = 10;
        }

        public int MaxRequestsPerRoute { get; set; }
        public int MaxErrorsPerRoute { get; set; }
    }
}