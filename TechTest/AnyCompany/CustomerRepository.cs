using System;
using System.Data.SqlClient;

namespace AnyCompany
{
    public static class CustomerRepository
    {
        private static string ConnectionString = @"Data Source=(local);Database=Customers;User Id=admin;Password=password;";

        public static Customer Load(int customerId)
        {
            var customer = new Customer();

            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                var command = new SqlCommand("SELECT * FROM Customer WHERE CustomerId = " + customerId,connection);
                using (var reader = command.ExecuteReader())
                { 
                    if (reader.Read())
                    {
                        customer.Name = reader["Name"].ToString();
                        customer.DateOfBirth = DateTime.Parse(reader["DateOfBirth"].ToString());
                        customer.Country = reader["Country"].ToString();
                        customer.CustomerId = customerId;
                    }
                }
            }
            return customer;
        }
    }
}
