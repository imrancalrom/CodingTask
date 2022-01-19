using System;
using System.Threading.Tasks;

namespace CodingTask.Infrastructure.Processing
{
    public interface ICommandsDispatcher
    {
        Task DispatchCommandAsync(Guid id);
    }
}
