using System;
using CodingTask.Application.Configuration;

namespace CodingTask.IntegrationTests.SeedWork
{
    public class ExecutionContextMock : IExecutionContextAccessor
    {
        public Guid CorrelationId { get; set; }

        public bool IsAvailable { get; set; }
    }
}