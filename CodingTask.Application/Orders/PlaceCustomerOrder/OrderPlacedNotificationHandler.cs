using System.Threading;
using System.Threading.Tasks;
using Dapper;
using MediatR;
using CodingTask.Application.Configuration.Data;
 
using CodingTask.Domain.Customers.Orders;

namespace CodingTask.Application.Orders.PlaceCustomerOrder
{
    public class OrderPlacedNotificationHandler : INotificationHandler<OrderPlacedNotification>
    {
       
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public OrderPlacedNotificationHandler(
             
            ISqlConnectionFactory sqlConnectionFactory)
        {
            
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task Handle(OrderPlacedNotification request, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            const string sql = "SELECT [Customer].[Email] " +
                               "FROM orders.v_Customers AS [Customer] " +
                               "WHERE [Customer].[Id] = @Id";

            var customerEmail = await connection.QueryFirstAsync<string>(sql, 
                new
                {
                    Id = request.CustomerId.Value
                });

             
        }
    }
}