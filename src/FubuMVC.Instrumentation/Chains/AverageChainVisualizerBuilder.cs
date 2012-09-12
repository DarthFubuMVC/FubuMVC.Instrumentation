using System;
using System.Collections.Generic;
using System.Linq;
using FubuMVC.Core.Registration;
using FubuMVC.Core.Registration.Nodes;
using FubuCore;
using FubuMVC.Instrumentation.Features.Instrumentation.Models;
using FubuMVC.Instrumentation.Runtime;

namespace FubuMVC.Instrumentation.Chains
{
    //TODO: Update to work with new structure:
    public class AverageChainVisualizerBuilder : IAverageChainVisualizerBuilder
    {
        private readonly BehaviorGraph _graph;
        private readonly IInstrumentationReportCache _cache;

        public AverageChainVisualizerBuilder(BehaviorGraph graph,
            IInstrumentationReportCache cache)
        {
            _graph = graph;
            _cache = cache;
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

            _cache.GetReport(uniqueId).Reports.Each(
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