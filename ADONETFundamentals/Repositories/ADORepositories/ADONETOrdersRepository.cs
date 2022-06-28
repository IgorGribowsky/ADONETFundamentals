using ADONETFundamentals.Constants;
using ADONETFundamentals.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace ADONETFundamentals.Repositories.ADORepositories
{
    public class ADONETOrdersRepository : IOrdersRepository
    {
        public int Create(Order entity)
        {
            string sqlExpression = $"INSERT INTO Orders (Status, CreatedDate, UpdatedDate, ProductId) " +
                $"VALUES ('{entity.Status}', '{entity.CreatedDate}', '{entity.UpdatedDate}', {entity.ProductId}); " +
                $"SELECT CAST(scope_identity() AS int)";

            int id = 0;
            using (SqlConnection connection = new SqlConnection(AppConstants.ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                id = (int)command.ExecuteScalar();
            }

            entity.Id = id;
            return id;
        }

        public void Delete(int id)
        {
            string sqlExpression = $"DELETE FROM Orders WHERE Id={id}";

            using (SqlConnection connection = new SqlConnection(AppConstants.ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.ExecuteNonQuery();
            }
        }

        public void DeleteOrdersByMonth(int month)
        {
            using (SqlConnection connection = new SqlConnection(AppConstants.ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(StoredProceduresConstants.DeleteOrdersByMonth, connection);

                command.CommandType = System.Data.CommandType.StoredProcedure;

                SqlParameter monthParameter = new SqlParameter
                {
                    ParameterName = StoredProceduresConstants.MonthParameter,
                    Value = month
                };

                command.Parameters.Add(monthParameter);

                command.ExecuteScalar();
            }
        }

        public void DeleteOrdersByProduct(string productName)
        {
            using (SqlConnection connection = new SqlConnection(AppConstants.ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(StoredProceduresConstants.DeleteOrdersByProduct, connection);

                command.CommandType = System.Data.CommandType.StoredProcedure;

                SqlParameter productNameParameter = new SqlParameter
                {
                    ParameterName = StoredProceduresConstants.ProductNameParameter,
                    Value = productName
                };

                command.Parameters.Add(productNameParameter);

                command.ExecuteScalar();
            }
        }

        public void DeleteOrdersByStatus(string status)
        {
            using (SqlConnection connection = new SqlConnection(AppConstants.ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(StoredProceduresConstants.DeleteOrdersByStatus, connection);

                command.CommandType = System.Data.CommandType.StoredProcedure;

                SqlParameter statusParameter = new SqlParameter
                {
                    ParameterName = StoredProceduresConstants.StatusParameter,
                    Value = status
                };
                command.Parameters.Add(statusParameter);

                command.ExecuteScalar();
            }
        }

        public void DeleteOrdersByYear(int year)
        {
            using (SqlConnection connection = new SqlConnection(AppConstants.ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(StoredProceduresConstants.DeleteOrdersByYear, connection);

                command.CommandType = System.Data.CommandType.StoredProcedure;

                SqlParameter yearParam = new SqlParameter
                {
                    ParameterName = StoredProceduresConstants.YearParameter,
                    Value = year
                };
                command.Parameters.Add(yearParam);

                command.ExecuteScalar();
            }
        }

        public IEnumerable<Order> GetAll()
        {
            string sqlExpression = "SELECT * FROM Orders";

            var orders = new List<Order>();

            using (SqlConnection connection = new SqlConnection(AppConstants.ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();

                ReadOrders(reader, orders);
            }

            return orders;
        }

        public Order GetById(int id)
        {
            string sqlExpression = "SELECT * FROM Orders" +
                $" WHERE Id = {id}";

            Order order = null;

            using (SqlConnection connection = new SqlConnection(AppConstants.ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    string status = (string)reader.GetValue(1);
                    DateTime createdDate = (DateTime)reader.GetValue(2);
                    DateTime updatedDate = (DateTime)reader.GetValue(3);
                    int productId = (int)reader.GetValue(4);

                    order = new Order
                    {
                        Id = id,
                        Status = status,
                        CreatedDate = createdDate,
                        UpdatedDate = updatedDate,
                        ProductId = productId,
                    };
                }
            }

            return order;
        }

        public IEnumerable<Order> GetOrdersByMonth(int month)
        {
            var orders = new List<Order>();

            using (var connection = new SqlConnection(AppConstants.ConnectionString))
            {
                connection.Open();
                var command = new SqlCommand(StoredProceduresConstants.GetOrdersByMonth, connection);

                command.CommandType = System.Data.CommandType.StoredProcedure;

                var monthParameter = new SqlParameter
                {
                    ParameterName = StoredProceduresConstants.MonthParameter,
                    Value = month
                };

                command.Parameters.Add(monthParameter);

                SqlDataReader reader = command.ExecuteReader();

                ReadOrders(reader, orders);
            }

            return orders;
        }

        public IEnumerable<Order> GetOrdersByProduct(string productName)
        {
            var orders = new List<Order>();

            using (SqlConnection connection = new SqlConnection(AppConstants.ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(StoredProceduresConstants.GetOrdersByProduct, connection);

                command.CommandType = System.Data.CommandType.StoredProcedure;

                SqlParameter productNameParameter = new SqlParameter
                {
                    ParameterName = StoredProceduresConstants.ProductNameParameter,
                    Value = productName
                };

                command.Parameters.Add(productNameParameter);

                SqlDataReader reader = command.ExecuteReader();

                ReadOrders(reader, orders);
            }

            return orders;
        }

        public IEnumerable<Order> GetOrdersByStatus(string status)
        {
            var orders = new List<Order>();

            using (SqlConnection connection = new SqlConnection(AppConstants.ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(StoredProceduresConstants.GetOrdersByStatus, connection);

                command.CommandType = System.Data.CommandType.StoredProcedure;

                SqlParameter statusParameter = new SqlParameter
                {
                    ParameterName = StoredProceduresConstants.StatusParameter,
                    Value = status
                };
                command.Parameters.Add(statusParameter);

                SqlDataReader reader = command.ExecuteReader();

                ReadOrders(reader, orders);
            }

            return orders;
        }

        public IEnumerable<Order> GetOrdersByYear(int year)
        {
            var orders = new List<Order>();

            using (SqlConnection connection = new SqlConnection(AppConstants.ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(StoredProceduresConstants.GetOrdersByYear, connection);

                command.CommandType = System.Data.CommandType.StoredProcedure;

                SqlParameter yearParam = new SqlParameter
                {
                    ParameterName = StoredProceduresConstants.YearParameter,
                    Value = year
                };
                command.Parameters.Add(yearParam);

                SqlDataReader reader = command.ExecuteReader();

                ReadOrders(reader, orders);
            }

            return orders;
        }

        public void Update(Order entity)
        {
            string sqlExpression = $"UPDATE Orders " +
                $"SET Status = '{entity.Status}', " +
                $"CreatedDate = '{entity.CreatedDate}', " +
                $"UpdatedDate = '{entity.UpdatedDate}', " +
                $"ProductId = {entity.ProductId} " +
                $"WHERE Id={entity.Id}";

            using (SqlConnection connection = new SqlConnection(AppConstants.ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.ExecuteNonQuery();
            }
        }

        private void ReadOrders(SqlDataReader reader, IList<Order> orders)
        {
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    int id = (int)reader.GetValue(0);
                    string status = (string)reader.GetValue(1);
                    DateTime createdDate = (DateTime)reader.GetValue(2);
                    DateTime updatedDate = (DateTime)reader.GetValue(3);
                    int productId = (int)reader.GetValue(4);

                    var order = new Order
                    {
                        Id = id,
                        Status = status,
                        CreatedDate = createdDate,
                        UpdatedDate = updatedDate,
                        ProductId = productId,
                    };
                    orders.Add(order);
                }
            }
        }
    }
}
