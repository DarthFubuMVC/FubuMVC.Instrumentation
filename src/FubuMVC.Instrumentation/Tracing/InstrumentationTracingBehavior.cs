using System;
using FubuMVC.Core.Behaviors;
using FubuMVC.Core.Runtime;
using FubuMVC.Core.Runtime.Logging;
using FubuMVC.Diagnostics.Runtime;
using FubuMVC.Diagnostics.Runtime.Tracing;

namespace FubuMVC.Instrumentation.Tracing
{
    public class InstrumentationTracingBehavior : WrappingBehavior
    {
        private readonly IInstrumentationRequestTrace _trace;
        private readonly IDebugDetector _detector;
        private readonly IOutputWriter _writer;

        public InstrumentationTracingBehavior(IInstrumentationRequestTrace trace,
            IDebugDetector detector,
            IOutputWriter writer)
        {
            _trace = trace;
            _detector = detector;
            _writer = writer;
        }

        protected override void invoke(Action action)
        {
            _trace.Start();

            try
            {
                action();
            }
            catch (UnhandledFubuException ex)
            {
                _trace.MarkAsFailedRequest();
                throw ex.InnerException;
            }
            catch (Exception ex)
            {
                _trace.MarkAsFailedRequest();
                _trace.Log(new ExceptionReport("Request failed", ex));
                throw;
            }
            finally
            {
                _trace.MarkFinished();

                if (_detector.IsDebugCall())
                {
                    _writer.RedirectToUrl(_trace.LogUrl);
                }
            }

        }
    }
}