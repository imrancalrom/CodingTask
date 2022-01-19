using CodingTask.Domain.SharedKernel;

namespace CodingTask.Domain.Products
{
    public class ProductPrice
    {
        public MoneyValue Value { get; private set; }

        private ProductPrice()
        {
            
        }
    }
}