using System.Threading;
using System.Threading.Tasks;
using MediatR;
using CodingTask.Domain.Customers.Orders.Events;
using CodingTask.Domain.Payments;

namespace CodingTask.Application.Orders.PlaceCustomerOrder
{
    public class OrderPlacedDomainEventHandler : INotificationHandler<OrderPlacedEvent>
    {
        private readonly IPaymentRepository _paymentRepository;

        public OrderPlacedDomainEventHandler(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task Handle(OrderPlacedEvent notification, CancellationToken cancellationToken)
        {
            var newPayment = new Payment(notification.OrderId);

            await this._paymentRepository.AddAsync(newPayment);
        }
    }
}