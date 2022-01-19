using System;
using System.Collections.Generic;
using System.Linq;
using CodingTask.Domain.SeedWork;
using CodingTask.Domain.SharedKernel;

namespace CodingTask.Domain.Products
{
    public class Product : Entity, IAggregateRoot
    {
        public ProductId Id { get; private set; }

        public string Name { get; private set; }

        private List<ProductPrice> _prices;

        private Product()
        {

        }
    }
}