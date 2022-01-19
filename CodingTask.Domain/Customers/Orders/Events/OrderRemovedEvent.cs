using CodingTask.Domain.SeedWork;

namespace CodingTask.Domain.Customers.Orders.Events
{
    public class OrderRemovedEvent : DomainEventBase
    {
        public OrderId OrderId { get; }

        public OrderRemovedEvent(OrderId orderId)
        {
            this.OrderId = orderId;
        }
    }
}