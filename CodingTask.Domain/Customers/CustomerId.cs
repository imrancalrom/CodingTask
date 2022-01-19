using System;
using CodingTask.Domain.SeedWork;

namespace CodingTask.Domain.Customers
{
    public class CustomerId : TypedIdValueBase
    {
        public CustomerId(Guid value) : base(value)
        {
        }
    }
}