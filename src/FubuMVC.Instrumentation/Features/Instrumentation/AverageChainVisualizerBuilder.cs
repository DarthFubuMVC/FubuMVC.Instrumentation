using System;
using System.Collections.Generic;
using System.Linq;
using FubuMVC.Core.Registration;
using FubuMVC.Core.Registration.Nodes;
using FubuCore;
using FubuMVC.Instrumentation.Diagnostics;
using FubuMVC.Instrumentation.Features.Instrumentation.Models;

namespace FubuMVC.Instrumentation.Features.Instrumentation
{
    public class AverageChainVisualizerBuilder : IAverageChainVisualizerBuilder
    {
        private readonly BehaviorGraph _graph;
        //private readonly IHttpConstraintResolver _constraintResolver;
        private readonly IInstrumentationReportCache _instrumentationReportCache;

        public AverageChainVisualizerBuilder(BehaviorGraph graph,
            /*IHttpConstraintResolver constraintResolver,*/
            IInstrumentationReportCache instrumentationReportCache)
        {
            _graph = graph;
            //_constraintResolver = constraintResolver;
            _instrumentationReportCache = instrumentationReportCache;
        }

        public AverageChainModel VisualizerFor(Guid uniqueId)
        {
            var chain = _graph
                .Behaviors
                .SingleOrDefault(c => c.UniqueId == uniqueId);

            if (chain == null)
            {
                return null;
            }

            return new AverageChainModel
            {
                Chain = chain,
                //Constraints = _constraintResolver.Resolve(chain),
                BehaviorAverages = BuildBehaviorAverages(uniqueId, chain)
            };
        }

        private IEnumerable<AverageBehaviorModel> BuildBehaviorAverages(Guid uniqueId, BehaviorChain chain)
        {
            var keyedAverages = new Dictionary<Guid, AverageBehaviorModel>();
            var averages = chain.Select(c =>
            {
                var behavior = new AverageBehaviorModel
                {
                    Id = c.UniqueId,
                    DisplayType = c.GetType().PrettyPrint(),
                    BehaviorType = c.ToString()
                };

                keyedAverages.Add(c.UniqueId, behavior);
                return behavior;
            }).ToList();

            _instrumentationReportCache.GetReport(uniqueId).Reports.Each(
                debugReport => debugReport.AllSteps().Each(behaviorReport =>
                {
                    AverageBehaviorModel model;
                    if (keyedAverages.TryGetValue(behaviorReport.Id, out model))
                    {
                        model.HitCount++;
                        model.TotalExecutionTime += behaviorReport.RequestTimeInMilliseconds;
                    }
                }));
            return averages;
        }
    }
}