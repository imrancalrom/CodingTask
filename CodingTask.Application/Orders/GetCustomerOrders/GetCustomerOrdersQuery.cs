using System;
using System.Collections.Generic;
using MediatR;
using CodingTask.Application.Configuration.Queries;

namespace CodingTask.Application.Orders.GetCustomerOrders
{
    public class GetCustomerOrdersQuery : IQuery<List<OrderDto>>
    {
        public Guid CustomerId { get; }

        public GetCustomerOrdersQuery(Guid customerId)
        {
            this.CustomerId = customerId;
        }
    }
}