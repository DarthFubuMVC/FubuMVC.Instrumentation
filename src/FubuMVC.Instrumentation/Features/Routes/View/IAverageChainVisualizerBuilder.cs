using System;
using FubuMVC.Instrumentation.Features.Routes.Models;

namespace FubuMVC.Instrumentation.Features.Routes.View
{
    public interface IAverageChainVisualizerBuilder
    {
        AverageChainModel VisualizerFor(Guid uniqueId);
    }
}