using System;
using MediatR;
using CodingTask.Application.Configuration.Queries;

namespace CodingTask.Application.Orders.GetCustomerOrderDetails
{
    public class GetCustomerOrderDetailsQuery : IQuery<OrderDetailsDto>
    {
        public Guid OrderId { get; }

        public GetCustomerOrderDetailsQuery(Guid orderId)
        {
            this.OrderId = orderId;
        }
    }
}