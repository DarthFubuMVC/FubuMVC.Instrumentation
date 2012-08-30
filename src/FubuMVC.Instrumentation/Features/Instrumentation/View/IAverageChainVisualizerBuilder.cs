using System;
using FubuMVC.Instrumentation.Features.Instrumentation.Models;

namespace FubuMVC.Instrumentation.Features.Instrumentation.View
{
    public interface IAverageChainVisualizerBuilder
    {
        AverageChainModel VisualizerFor(Guid uniqueId);
    }
}