using System;
using FubuMVC.Instrumentation.Features.Instrumentation.Models;

namespace FubuMVC.Instrumentation.Chains
{
    public interface IAverageChainVisualizerBuilder
    {
        AverageChainModel VisualizerFor(Guid uniqueId);
    }
}