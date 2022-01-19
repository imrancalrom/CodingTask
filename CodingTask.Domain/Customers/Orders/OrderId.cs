using System;
using CodingTask.Domain.SeedWork;

namespace CodingTask.Domain.Customers.Orders
{
    public class OrderId : TypedIdValueBase
    {
        public OrderId(Guid value) : base(value)
        {
        }
    }
}