using System.Threading.Tasks;
using MediatR;
using CodingTask.Application.Configuration.Commands;

namespace CodingTask.Application.Configuration.Processing
{
    public interface ICommandsScheduler
    {
        Task EnqueueAsync<T>(ICommand<T> command);
    }
}