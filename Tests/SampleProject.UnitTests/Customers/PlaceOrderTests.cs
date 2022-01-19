using System;
using System.Collections.Generic;
using NUnit.Framework;
using CodingTask.Domain.Customers.Orders;
using CodingTask.Domain.Customers.Orders.Events;
using CodingTask.Domain.Customers.Rules;
using CodingTask.Domain.Products;
using CodingTask.Domain.SharedKernel;
using CodingTask.UnitTests.SeedWork;

namespace CodingTask.UnitTests.Customers
{
    [TestFixture]
    public class PlaceOrderTests : TestBase
    {
        [Test]
        public void PlaceOrder_WhenAtLeastOneProductIsAdded_IsSuccessful()
        {
            // Arrange
            var customer = CustomerFactory.Create();

            var orderProductsData = new List<OrderProductData>();
            orderProductsData.Add(new OrderProductData(SampleProducts.Product1Id, 2));
            
            var allProductPrices = new List<ProductPriceData>
            {
                SampleProductPrices.Product1EUR, SampleProductPrices.Product1USD
            };
            
            const string currency = "EUR";
          
            
            // Act
            customer.PlaceOrder(
                orderProductsData, 
                allProductPrices, 
                currency);

            // Assert
            var orderPlaced = AssertPublishedDomainEvent<OrderPlacedEvent>(customer);
            Assert.That(orderPlaced.Value, Is.EqualTo(MoneyValue.Of(200, "EUR")));
        }

        [Test]
        public void PlaceOrder_WhenNoProductIsAdded_BreaksOrderMustHaveAtLeastOneProductRule()
        {
            // Arrange
            var customer = CustomerFactory.Create();

            var orderProductsData = new List<OrderProductData>();

            var allProductPrices = new List<ProductPriceData>
            {
                SampleProductPrices.Product1EUR, SampleProductPrices.Product1USD
            };

            const string currency = "EUR";
          

            // Assert
            AssertBrokenRule<OrderMustHaveAtLeastOneProductRule>(() =>
            {
                // Act
                customer.PlaceOrder(
                    orderProductsData,
                    allProductPrices,
                    currency);
            });
        }

        [Test]
        public void PlaceOrder_GivenTwoOrdersInThatDayAlreadyMade_BreaksCustomerCannotOrderMoreThan2OrdersOnTheSameDayRule()
        {
            // Arrange
            var customer = CustomerFactory.Create();

            var orderProductsData = new List<OrderProductData>();
            orderProductsData.Add(new OrderProductData(SampleProducts.Product1Id, 2));

            var allProductPrices = new List<ProductPriceData>
            {
                SampleProductPrices.Product1EUR, SampleProductPrices.Product1USD
            };

            const string currency = "EUR";
           

            SystemClock.Set(new DateTime(2020, 1, 10, 11, 0, 0));
            customer.PlaceOrder(
                orderProductsData,
                allProductPrices,
                currency);

            SystemClock.Set(new DateTime(2020, 1, 10, 11, 30, 0));
            customer.PlaceOrder(
                orderProductsData,
                allProductPrices,
                currency);

            SystemClock.Set(new DateTime(2020, 1, 10, 12, 00, 0));

            // Assert
            AssertBrokenRule<CustomerCannotOrderMoreThan2OrdersOnTheSameDayRule>(() =>
            {
                // Act
                customer.PlaceOrder(
                    orderProductsData,
                    allProductPrices,
                    currency);
            });
        }

     
    }



    public class SampleProducts
    {
        public static readonly ProductId Product1Id = new ProductId(Guid.NewGuid());

        public static readonly ProductId Product2Id = new ProductId(Guid.NewGuid());
    }

    public class SampleProductPrices
    {
        public static readonly ProductPriceData Product1EUR = new ProductPriceData(
            SampleProducts.Product1Id,
            MoneyValue.Of(100, "EUR"));

        public static readonly ProductPriceData Product1USD = new ProductPriceData(
            SampleProducts.Product1Id,
            MoneyValue.Of(110, "USD"));
    }
}