using MediatR;
using CodingTask.Application;
using CodingTask.Application.Configuration.Commands;

namespace CodingTask.Infrastructure.Processing.Outbox
{
    public class ProcessOutboxCommand : CommandBase<Unit>, IRecurringCommand
    {

    }
}