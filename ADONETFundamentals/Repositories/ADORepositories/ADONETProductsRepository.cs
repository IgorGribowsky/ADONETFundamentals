using ADONETFundamentals.Constants;
using ADONETFundamentals.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace ADONETFundamentals.Repositories.ADORepositories
{
    public class ADONETProductsRepository : IProductsRepository
    {
        public int Create(Product entity)
        {
            string sqlExpression = $"INSERT INTO Products (Name, Description, Height, Weight, Width, Length) " +
                $"VALUES ('{entity.Name}', '{entity.Description}', {entity.Height}, {entity.Weight}, {entity.Width}, {entity.Length}); " +
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
            string sqlExpression = $"DELETE FROM Products WHERE Id={id}";

            using (SqlConnection connection = new SqlConnection(AppConstants.ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.ExecuteNonQuery();
            }
        }

        public IEnumerable<Product> GetAll()
        {
            string sqlExpression = "SELECT * FROM Products";

            var products = new List<Product>();

            using (SqlConnection connection = new SqlConnection(AppConstants.ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int id = (int)reader.GetValue(0);
                        string name = (string)reader.GetValue(1);
                        string description = (string)reader.GetValue(2);
                        double weight = (double)reader.GetValue(3);
                        double height = (double)reader.GetValue(4);
                        double width = (double)reader.GetValue(5);
                        double length = (double)reader.GetValue(6);

                        var product = new Product
                        {
                            Id = id,
                            Name = name,
                            Description = description,
                            Weight = weight,
                            Height = height,
                            Width = width,
                            Length = length,
                        };
                        products.Add(product);
                    }
                }
            }

            return products;
        }

        public Product GetById(int id)
        {
            string sqlExpression = "SELECT * FROM Products" +
                $" WHERE Id = {id}";

            Product product = null;

            using (SqlConnection connection = new SqlConnection(AppConstants.ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    string name = (string)reader.GetValue(1);
                    string description = (string)reader.GetValue(2);
                    double weight = (double)reader.GetValue(3);
                    double height = (double)reader.GetValue(4);
                    double width = (double)reader.GetValue(5);
                    double length = (double)reader.GetValue(6);

                    product = new Product
                    {
                        Id = id,
                        Name = name,
                        Description = description,
                        Weight = weight,
                        Height = height,
                        Width = width,
                        Length = length,
                    };
                }
            }

            return product;
        }

        public void Update(Product entity)
        {
            string sqlExpression = $"UPDATE Products " +
                $"SET Name = '{entity.Name}', " +
                $"Description = '{entity.Description}', " +
                $"Height = {entity.Height}, " +
                $"Weight = {entity.Weight}, " +
                $"Width = {entity.Width}, " +
                $"Length = {entity.Length} " +
                $"WHERE Id={entity.Id}";

            using (SqlConnection connection = new SqlConnection(AppConstants.ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.ExecuteNonQuery();
            }
        }
    }
}
