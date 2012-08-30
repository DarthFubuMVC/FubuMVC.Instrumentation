using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using FubuMVC.Core;
using FubuMVC.Diagnostics.Runtime;

namespace FubuMVC.Instrumentation.Diagnostics
{
    public class RouteInstrumentationReport
    {
        private long _exceptionCount;
        private long _hitCount;
        private long _minExecution = long.MaxValue;
        private long _maxExecution;
        private long _totalExecutionTime;
        public IList<RequestLog> Reports { get { return _reportCache.ToArray(); } }
        private readonly ConcurrentQueue<RequestLog> _reportCache;
        private readonly DiagnosticsSettings _settings;

        public decimal AverageExecution { get { return _totalExecutionTime * 1m / _hitCount; } }
        public long ExceptionCount { get { return _exceptionCount; } }
        public long HitCount { get { return _hitCount; } }
        public long MinExecution { get { return _minExecution; } }
        public long MaxExecution { get { return _maxExecution; } }

        public string Url { get; set; }
        public string ReportUrl { get; set; }
        public Guid Id { get; private set; }

        public RouteInstrumentationReport(DiagnosticsSettings settings, Guid behaviorId, string route = null)
        {
            _settings = settings;
            _reportCache = new ConcurrentQueue<RequestLog>();
            Id = behaviorId;
            Url = route;
        }

        public void AddReportLog(RequestLog report)
        {
            if (report.Failed)
            {
                IncrementExceptionCount();
            }

            IncrementHitCount();
            AddExecutionTime((long)report.ExecutionTime);

            _reportCache.Enqueue(report);

            while (_reportCache.Count > _settings.MaxRequests)
            {
                RequestLog r;
                _reportCache.TryDequeue(out r);
            }
        }

        public void IncrementHitCount()
        {
            Interlocked.Increment(ref _hitCount);
        }

        public void IncrementExceptionCount()
        {
            Interlocked.Increment(ref _exceptionCount);
        }

        public void AddExecutionTime(long executionTime)
        {
            Interlocked.Add(ref _totalExecutionTime, executionTime);

            if (executionTime < Interlocked.Read(ref _minExecution))
            {
                Interlocked.Exchange(ref _minExecution, executionTime);
            }

            if (executionTime > Interlocked.Read(ref _maxExecution))
            {
                Interlocked.Exchange(ref _maxExecution, executionTime);
            }
        }
    }
}
