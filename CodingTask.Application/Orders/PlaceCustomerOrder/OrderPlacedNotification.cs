using Newtonsoft.Json;
using CodingTask.Application.Configuration.DomainEvents;
using CodingTask.Domain.Customers;
using CodingTask.Domain.Customers.Orders;
using CodingTask.Domain.Customers.Orders.Events;

namespace CodingTask.Application.Orders.PlaceCustomerOrder
{
    public class OrderPlacedNotification : DomainNotificationBase<OrderPlacedEvent>
    {
        public OrderId OrderId { get; }
        public CustomerId CustomerId { get; }

        public OrderPlacedNotification(OrderPlacedEvent domainEvent) : base(domainEvent)
        {
            this.OrderId = domainEvent.OrderId;
            this.CustomerId = domainEvent.CustomerId;
        }

        [JsonConstructor]
        public OrderPlacedNotification(OrderId orderId, CustomerId customerId) : base(null)
        {
            this.OrderId = orderId;
            this.CustomerId = customerId;
        }
    }
}