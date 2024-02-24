using Microsoft.Data.SqlClient;
using MyEcomWebApplication.Application.Common.Models;

namespace MyEcomWebApplication.Application.DataAccess
{
    public class DataAccess
    {
        private readonly string _connectionString;
        public DataAccess(string connectionString)
        {
            _connectionString = connectionString;
        }
        public async Task<OrderResponseModel> GetMostRecentOrderAsync(string customerId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                // query to fetch order id, order date and delivery expected from orders table based on customer Id
                string query1 = @"
                SELECT TOP 1 
                OrderId, [Order Date], DeliveryExpected
                FROM Orders
                WHERE CustomerId = @CustomerId
                ORDER BY OrderDate DESC";

                // query to fetch customer details from customer table based on customer Id
                string query2 = @"
                SELECT FirstName,LastName FROM Customers WHERE CustomerId = @CustomerId";

                // fetch address based on customer id
                string query3 = @"
                SELECT CONCAT(HouseNo, ' ', Street, ', ', Town, ', ', PostCode) AS Address FROM Customers 
                WHERE CustomerId = @CustomerId";

                // query to fetch Name from products and Quantity and price from order items for particular order Id
                var query4 = @"
                SELECT p.Name AS ProductName,oi.Quantity,oi.Price FROM OrderItems oi INNER JOIN Products p ON
                oi.ProductId = p.ProductId WHERE oi.OrderId = @OrderId";

                using (var command = new SqlCommand(query1 +query2 +query3, connection))
                {
                    var orderResponse = new OrderResponseModel();
                    command.Parameters.AddWithValue("@CustomerId", customerId);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var order = new Order();
                           {
                                order.OrderNumber = reader.GetInt32(reader.GetOrdinal("OrderId"));
                                order.OrderDate = reader.GetString(reader.GetOrdinal("OrderDate"));
                                order.DeliveryExpected = reader.GetString(reader.GetOrdinal("DeliveryExpected"));
                                order.ContainsGift = reader.GetBoolean(reader.GetOrdinal("ContainsGift"));
                            }
                            orderResponse.order= order;
                        }
                        if (reader.NextResult())
                        {
                            while (await reader.ReadAsync())
                            {
                                var customer = new Customer();
                                {
                                    customer.FirstName= reader.GetString(reader.GetOrdinal("FirstName"));
                                    customer.LastName = reader.GetString(reader.GetOrdinal("LastName"));
                                }

                                orderResponse.customer = customer;

                            }
                            
                        }
                        if (reader.NextResult())
                        {
                            while (await reader.ReadAsync())
                            {
                                string address = reader.GetString(0);
                                orderResponse.order.DeliveryAddress = address;
                            }

                        }
                        command.Parameters.AddWithValue("@OrderId", orderResponse.order.OrderNumber);
                        if (reader.NextResult())
                        {
                            while (await reader.ReadAsync())
                            {
                                var itemDetails=new ItemDetails();
                                itemDetails.Product = reader.GetString(reader.GetOrdinal("Product"));
                                itemDetails.Quantity = reader.GetInt32(reader.GetOrdinal("Quantity"));
                                itemDetails.PriceEach = reader.GetInt32(reader.GetOrdinal("Price"));

                                orderResponse.order.OrderItems.Add(itemDetails);
                            }

                        }
    
                    }

                    return orderResponse;

                }
            }
        }
    }
}
