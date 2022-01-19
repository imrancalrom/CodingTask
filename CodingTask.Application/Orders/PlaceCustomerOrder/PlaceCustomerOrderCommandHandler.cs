using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CodingTask.Application.Configuration.Commands;
using CodingTask.Application.Configuration.Data;
using CodingTask.Domain.Customers;
using CodingTask.Domain.Customers.Orders;
using CodingTask.Domain.ForeignExchange;
using CodingTask.Domain.Products;

namespace CodingTask.Application.Orders.PlaceCustomerOrder
{
    public class PlaceCustomerOrderCommandHandler : ICommandHandler<PlaceCustomerOrderCommand, Guid>
    {
        private readonly ICustomerRepository _customerRepository;
        
        private readonly ISqlConnectionFactory _sqlConnectionFactory;
        
        private readonly IForeignExchange _foreignExchange;

        public PlaceCustomerOrderCommandHandler(
            ICustomerRepository customerRepository,
            IForeignExchange foreignExchange, 
            ISqlConnectionFactory sqlConnectionFactory)
        {
            this._customerRepository = customerRepository;
            this._foreignExchange = foreignExchange;
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<Guid> Handle(PlaceCustomerOrderCommand command, CancellationToken cancellationToken)
        {
            var customer = await this._customerRepository.GetByIdAsync(new CustomerId(command.CustomerId));

            var allProductPrices =
                await ProductPriceProvider.GetAllProductPrices(_sqlConnectionFactory.GetOpenConnection());

            var conversionRates = this._foreignExchange.GetConversionRates();

            var orderProductsData = command
                .Products
                .Select(x => new OrderProductData(new ProductId(x.Id), x.Quantity))
                .ToList();          
            
            var orderId = customer.PlaceOrder(
                orderProductsData,
                allProductPrices,
                command.Currency,
                conversionRates);

            return orderId.Value;
        }
    }
}