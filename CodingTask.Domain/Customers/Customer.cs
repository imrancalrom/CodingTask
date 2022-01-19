using System;
using System.Collections.Generic;
using System.Linq;
using CodingTask.Domain.Customers.Orders;
using CodingTask.Domain.Customers.Orders.Events;
using CodingTask.Domain.Customers.Rules;
using CodingTask.Domain.SeedWork;
using CodingTask.Domain.Products;
namespace CodingTask.Domain.Customers
{
    public class Customer : Entity, IAggregateRoot
    {
        public CustomerId Id { get; private set; }

        private string _email;

        private string _name;

        private readonly List<Order> _orders;

        private bool _welcomeEmailWasSent;

        private Customer()
        {
            this._orders = new List<Order>();
        }
         
        private Customer(string email, string name)
        {
            this.Id = new CustomerId(Guid.NewGuid());
            _email = email;
            _name = name;
            _welcomeEmailWasSent = false;
            _orders = new List<Order>();

            this.AddDomainEvent(new CustomerRegisteredEvent(this.Id));
        }

        public static Customer CreateRegistered(
            string email, 
            string name,
            ICustomerUniquenessChecker customerUniquenessChecker)
        {
            CheckRule(new CustomerEmailMustBeUniqueRule(customerUniquenessChecker, email));

            return new Customer(email, name);
        }

        public OrderId PlaceOrder(
         List<OrderProductData> orderProductsData,
         List<ProductPriceData> allProductPrices,
         string currency )
        {
            CheckRule(new CustomerCannotOrderMoreThan2OrdersOnTheSameDayRule(_orders));
            CheckRule(new OrderMustHaveAtLeastOneProductRule(orderProductsData));

            var order = Order.CreateNew(orderProductsData, allProductPrices, currency );

            this._orders.Add(order);

            this.AddDomainEvent(new OrderPlacedEvent(order.Id, this.Id, order.GetValue()));

            return order.Id;
        }

        public void ChangeOrder(
            OrderId orderId,
            List<ProductPriceData> existingProducts,
            List<OrderProductData> newOrderProductsData,             
            string currency)
        {
            CheckRule(new OrderMustHaveAtLeastOneProductRule(newOrderProductsData));

            var order = this._orders.Single(x => x.Id == orderId);
            order.Change(existingProducts, newOrderProductsData,  currency);

            this.AddDomainEvent(new OrderChangedEvent(orderId));
        }

        public void RemoveOrder(OrderId orderId)
        {
            var order = this._orders.Single(x => x.Id == orderId);
            order.Remove();

            this.AddDomainEvent(new OrderRemovedEvent(orderId));
        }
 
    }
}