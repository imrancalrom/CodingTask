using System.Collections.Generic;

namespace CodingTask.Domain.ForeignExchange
{
    public interface IForeignExchange
    {
        List<ConversionRate> GetConversionRates();
    }
}