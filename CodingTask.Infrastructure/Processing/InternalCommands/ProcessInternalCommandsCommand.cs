using MediatR;
using CodingTask.Application;
using CodingTask.Application.Configuration.Commands;
using CodingTask.Infrastructure.Processing.Outbox;

namespace CodingTask.Infrastructure.Processing.InternalCommands
{
    internal class ProcessInternalCommandsCommand : CommandBase<Unit>, IRecurringCommand
    {

    }
}