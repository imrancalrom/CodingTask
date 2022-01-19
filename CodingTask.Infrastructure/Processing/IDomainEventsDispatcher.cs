using System.Threading.Tasks;

namespace CodingTask.Infrastructure.Processing
{
    public interface IDomainEventsDispatcher
    {
        Task DispatchEventsAsync();
    }
}