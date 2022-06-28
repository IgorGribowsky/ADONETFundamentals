using ADONETFundamentals.Models;
using System.Collections.Generic;

namespace ADONETFundamentals.Repositories
{
    public interface IOrdersRepository
    {
        public int Create(Order entity);

        public void Update(Order entity);

        public Order GetById(int id);

        public void Delete(int id);

        public IEnumerable<Order> GetAll();

        public IEnumerable<Order> GetOrdersByMonth(int month);

        public IEnumerable<Order> GetOrdersByYear(int year);

        public IEnumerable<Order> GetOrdersByStatus(string status);

        public IEnumerable<Order> GetOrdersByProduct(string productName);

        public void DeleteOrdersByMonth(int month);

        public void DeleteOrdersByYear(int year);

        public void DeleteOrdersByStatus(string status);

        public void DeleteOrdersByProduct(string productName);

    }
}
