using System.Reflection;
using CodingTask.Application.Orders.PlaceCustomerOrder;

namespace CodingTask.Infrastructure.Processing
{
    internal static class Assemblies
    {
        public static readonly Assembly Application = typeof(PlaceCustomerOrderCommand).Assembly;
    }
}