using System;
using CodingTask.Domain.SeedWork;

namespace CodingTask.Domain.Products
{
    public class ProductId : TypedIdValueBase
    {
        public ProductId(Guid value) : base(value)
        {
        }
    }
}