using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using CodingTask.Application.Configuration.Commands;
using CodingTask.Application.Configuration.Data;
using CodingTask.Application.Orders.PlaceCustomerOrder;
using CodingTask.Domain.Customers;
using CodingTask.Domain.Customers.Orders;
using CodingTask.Domain.Products;

namespace CodingTask.Application.Orders.ChangeCustomerOrder
{
    internal sealed class ChangeCustomerOrderCommandHandler : ICommandHandler<ChangeCustomerOrderCommand,Unit>
    {
        private readonly ICustomerRepository _customerRepository;

      

        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        internal ChangeCustomerOrderCommandHandler(
            ICustomerRepository customerRepository,
             
            ISqlConnectionFactory sqlConnectionFactory)
        {
            this._customerRepository = customerRepository;
       
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<Unit> Handle(ChangeCustomerOrderCommand request, CancellationToken cancellationToken)
        {
            var customer = await this._customerRepository.GetByIdAsync(new CustomerId(request.CustomerId));

            var orderId = new OrderId(request.OrderId);

            
            var orderProducts = request
                    .Products
                    .Select(x => new OrderProductData(new ProductId(x.Id), x.Quantity))
                    .ToList();

            var allProductPrices =
                await ProductPriceProvider.GetAllProductPrices(_sqlConnectionFactory.GetOpenConnection());

            customer.ChangeOrder(
                orderId,
                allProductPrices, 
                orderProducts, 
                request.Currency);

            return Unit.Value;
        }
    }
}
