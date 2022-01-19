using System;
using MediatR;
using CodingTask.Application.Configuration.Commands;

namespace CodingTask.Application.Orders.RemoveCustomerOrder
{
    public class RemoveCustomerOrderCommand : CommandBase
    {
        public Guid CustomerId { get; }

        public Guid OrderId { get; }

        public RemoveCustomerOrderCommand(
            Guid customerId,
            Guid orderId)
        {
            this.CustomerId = customerId;
            this.OrderId = orderId;
        }
    }
}