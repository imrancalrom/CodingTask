using System.Collections.Generic;
using CodingTask.Application.Orders;

namespace CodingTask.API.Orders
{
    public class CustomerOrderRequest
    {
        public List<ProductDto> Products { get; set; }

        public string Currency { get; set; }
    }
}