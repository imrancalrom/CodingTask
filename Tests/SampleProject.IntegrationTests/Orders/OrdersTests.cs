using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using NUnit.Framework;
using CodingTask.Application.Customers.IntegrationHandlers;
using CodingTask.Application.Customers.RegisterCustomer;
using CodingTask.Application.Orders;
using CodingTask.Application.Orders.GetCustomerOrderDetails;
using CodingTask.Application.Orders.PlaceCustomerOrder;
using CodingTask.Domain.Customers;
using CodingTask.Domain.Customers.Orders;
using CodingTask.Infrastructure.Processing;
using CodingTask.IntegrationTests.SeedWork;

namespace CodingTask.IntegrationTests.Orders
{
    [TestFixture]
    public class OrdersTests : TestBase
    {
        [Test]
        public async Task PlaceOrder_Test()
        {
            var customerEmail = "email@email.com";
            var customer = await CommandsExecutor.Execute(new RegisterCustomerCommand(customerEmail, "Sample Customer"));

            List<ProductDto> products = new List<ProductDto>();
            var productId = Guid.Parse("9DB6E474-AE74-4CF5-A0DC-BA23A42E2566");
            products.Add(new ProductDto(productId, 2));
            var orderId = await CommandsExecutor.Execute(new PlaceCustomerOrderCommand(customer.Id, products, "EUR"));

            var orderDetails = await QueriesExecutor.Execute(new GetCustomerOrderDetailsQuery(orderId));

            Assert.That(orderDetails, Is.Not.Null);
            Assert.That(orderDetails.Value, Is.EqualTo(70));
            Assert.That(orderDetails.Products.Count, Is.EqualTo(1));
            Assert.That(orderDetails.Products[0].Quantity, Is.EqualTo(2));
            Assert.That(orderDetails.Products[0].Id, Is.EqualTo(productId));

            var connection = new SqlConnection(ConnectionString);
            var messagesList = await OutboxMessagesHelper.GetOutboxMessages(connection);
            
            Assert.That(messagesList.Count, Is.EqualTo(3));
        

         

          
        }
    }
}