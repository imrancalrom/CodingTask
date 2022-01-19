using CodingTask.Domain.Products;

namespace CodingTask.Domain.Customers.Orders
{
    public class OrderProductData
    {
        public OrderProductData(ProductId productId, int quantity)
        {
            ProductId = productId;
            Quantity = quantity;
        }

        public ProductId ProductId { get; }

        public int Quantity { get; }
    }
}