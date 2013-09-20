using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using FubuMVC.Diagnostics.Runtime;

namespace FubuMVC.Instrumentation.Runtime
{
    public class RouteInstrumentationReport
    {
        private long _exceptionCount;
        private long _hitCount;
        private long _minExecution = long.MaxValue;
        private long _maxExecution;
        private long _totalExecutionTime;
        public IList<RequestLog> Reports { get { return _reportCache.Concat(_reportErrorCache).ToArray(); } }
        private readonly ConcurrentQueue<RequestLog> _reportCache;
        private readonly ConcurrentQueue<RequestLog> _reportErrorCache;
        private readonly InstrumentationSettings _settings;

        public decimal AverageExecution { get { return _totalExecutionTime * 1m / _hitCount; } }
        public long ExceptionCount { get { return _exceptionCount; } }
        public long HitCount { get { return _hitCount; } }
        public long MinExecution { get { return _minExecution; } }
        public long MaxExecution { get { return _maxExecution; } }
        public int MaxStoredRequests { get { return _settings.MaxRequestsPerRoute; } }

        public string Endpoint { get; set; }
        public string ReportUrl { get; set; }
        public Guid Id { get; private set; }

        public RouteInstrumentationReport(InstrumentationSettings settings, Guid behaviorId, string endpoint = null)
        {
            _settings = settings;
            _reportCache = new ConcurrentQueue<RequestLog>();
            _reportErrorCache = new ConcurrentQueue<RequestLog>();
            Id = behaviorId;
            Endpoint = endpoint;
        }

        public RouteInstrumentationReport AddReportLog(RequestLog report)
        {
            if (report.Failed)
            {
                IncrementExceptionCount();
            }

            IncrementHitCount();
            AddExecutionTime((long)report.ExecutionTime);

            _reportCache.Enqueue(report);

            while (_reportCache.Count > _settings.MaxRequestsPerRoute)
            {
                RequestLog r;
                if (_reportCache.TryDequeue(out r))
                {
                    if (r.Failed)
                    {
                        AddToErrorLog(r);
                    }
                }
            }
            return this;
        }

        private void AddToErrorLog(RequestLog report)
        {
            _reportErrorCache.Enqueue(report);
            while (_reportErrorCache.Count > _settings.MaxErrorsPerRoute)
            {
                RequestLog r;
                _reportErrorCache.TryDequeue(out r);
            }
        }

        private void IncrementHitCount()
        {
            Interlocked.Increment(ref _hitCount);
        }

        private void IncrementExceptionCount()
        {
            Interlocked.Increment(ref _exceptionCount);
        }

        private void AddExecutionTime(long executionTime)
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
