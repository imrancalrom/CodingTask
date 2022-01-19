﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodingTask.Domain.Products
{
    public interface IProductRepository
    {
        Task<List<Product>> GetByIdsAsync(List<ProductId> ids);

        Task<List<Product>> GetAllAsync();
    }
}