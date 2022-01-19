using MediatR;

namespace CodingTask.Application.Configuration.Queries
{
    public interface IQuery<out TResult> : IRequest<TResult>
    {

    }
}