using System;
using System.Collections.Generic;
using System.Linq;
 
using CodingTask.Domain.Products;
using CodingTask.Domain.SeedWork;
using CodingTask.Domain.SharedKernel;

namespace CodingTask.Domain.Customers.Orders
{
    public class OrderProduct : Entity
    {
        public int Quantity { get; private set; }

        public ProductId ProductId { get; private set; }

        internal MoneyValue Value { get; private set; }

        internal MoneyValue ValueInEUR { get; private set; }

        private OrderProduct()
        {

        }

        private OrderProduct(
            ProductPriceData productPrice,
            int quantity,
            string currency )
        {
            this.ProductId = productPrice.ProductId;
            this.Quantity = quantity;

            this.CalculateValue(productPrice, currency );
        }

        internal static OrderProduct CreateForProduct(
            ProductPriceData productPrice, int quantity, string currency )
        {
            return new OrderProduct(productPrice, quantity, currency );
        }

        internal void ChangeQuantity(ProductPriceData productPrice, int quantity )
        {
            this.Quantity = quantity;

            this.CalculateValue(productPrice, this.Value.Currency );
        }

        private void CalculateValue(ProductPriceData productPrice, string currency)
        {
           
        }
    }
}