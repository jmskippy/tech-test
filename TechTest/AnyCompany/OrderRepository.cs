using System.Collections.Generic;
using System.Data.SqlClient;

namespace AnyCompany
{
    internal class OrderRepository
    {
        private static string ConnectionString = @"Data Source=(local);Database=Orders;User Id=admin;Password=password;";

        public void Save(Order order, Customer customer)
        {
            using (var connection = new SqlConnection(ConnectionString))
            { 
                connection.Open();

                var command = new SqlCommand("INSERT INTO Orders VALUES (@OrderId, @Amount, @VAT, @CustomerId)", connection);

                command.Parameters.AddWithValue("@OrderId", order.OrderId);
                command.Parameters.AddWithValue("@Amount", order.Amount);
                command.Parameters.AddWithValue("@VAT", order.VAT);
                command.Parameters.AddWithValue("@CustomerId", customer.CustomerId);

                command.ExecuteNonQuery();
            }
        }

        public List<Order> LoadAll()
        {
            var orders = new List<Order>();

            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                var command = new SqlCommand("SELECT Orders.OrderId, Orders.CustomerId, Customer.Name, Orders.Amount, Orders.VAT " +
                                             "FROM Orders, Customer " +
                                             "WHERE Customer.CustomerId = Orders.CustomerId ", connection);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var order = new Order
                        {
                            OrderId = (int) reader["OrderId"],
                            CustomerId = (int) reader["CustomerId"],
                            CustomerName = reader["Name"].ToString(),
                            Amount = (double) reader["Amount"],
                            VAT = (double) reader["VAT"]
                        };
                        orders.Add(order);
                    }
                }
            }
            return orders;
        }
    }
}
