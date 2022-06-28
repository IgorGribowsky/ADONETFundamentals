using ADONETFundamentals.Constants;
using ADONETFundamentals.Models;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ADONETFundamentals.Repositories.EFRepositories
{
    public class EFOrdersRepository : IOrdersRepository
    {
        private readonly ApplicationContext _appContext = new ApplicationContext();

        public EFOrdersRepository(ApplicationContext appContext)
        {
            _appContext = appContext;
        }

        public int Create(Order entity)
        {
            _appContext.Orders.Add(entity);
            _appContext.SaveChanges();
            return entity.Id;
        }

        public void Delete(int id)
        {
            var entity = _appContext.Orders.Find(id);
            _appContext.Orders.Remove(entity);
            _appContext.SaveChanges();
        }

        public void DeleteOrdersByMonth(int month)
        {
            var monthParameter = new SqlParameter(StoredProceduresConstants.MonthParameter, month);
            _appContext.Database.ExecuteSqlRaw($"{StoredProceduresConstants.DeleteOrdersByMonth} {StoredProceduresConstants.MonthParameter}", monthParameter);
        }

        public void DeleteOrdersByProduct(string productName)
        {
            var productNameParameter = new SqlParameter(StoredProceduresConstants.ProductNameParameter, productName);
            _appContext.Database.ExecuteSqlRaw($"{StoredProceduresConstants.DeleteOrdersByProduct} {StoredProceduresConstants.ProductNameParameter}", productNameParameter);
        }

        public void DeleteOrdersByStatus(string status)
        {
            var statusParameter = new SqlParameter(StoredProceduresConstants.StatusParameter, status);
            _appContext.Database.ExecuteSqlRaw($"{StoredProceduresConstants.DeleteOrdersByStatus} {StoredProceduresConstants.StatusParameter}", statusParameter);
        }

        public void DeleteOrdersByYear(int year)
        {
            var yearParameter = new SqlParameter(StoredProceduresConstants.YearParameter, year);
            _appContext.Database.ExecuteSqlRaw($"{StoredProceduresConstants.DeleteOrdersByYear} {StoredProceduresConstants.YearParameter}", yearParameter);
        }

        public IEnumerable<Order> GetAll()
        {
            var query = _appContext.Orders;
            return query;
        }

        public Order GetById(int id)
        {
            var entity = _appContext.Orders.Find(id);
            return entity;
        }
        public IEnumerable<Order> GetOrdersByMonth(int month)
        {
            var monthParameter = new SqlParameter(StoredProceduresConstants.MonthParameter, month);
            var result = _appContext.Orders.FromSqlRaw($"{StoredProceduresConstants.GetOrdersByMonth} {StoredProceduresConstants.MonthParameter}", monthParameter);
            return result;
        }

        public IEnumerable<Order> GetOrdersByProduct(string productName)
        {
            var productNameParameter = new SqlParameter(StoredProceduresConstants.ProductNameParameter, productName);
            var result = _appContext.Orders.FromSqlRaw($"{StoredProceduresConstants.GetOrdersByProduct} {StoredProceduresConstants.ProductNameParameter}", productNameParameter);
            return result;
        }

        public IEnumerable<Order> GetOrdersByStatus(string status)
        {
            var statusParameter = new SqlParameter(StoredProceduresConstants.StatusParameter, status);
            var result = _appContext.Orders.FromSqlRaw($"{StoredProceduresConstants.GetOrdersByStatus} {StoredProceduresConstants.StatusParameter}", statusParameter);
            return result;
        }

        public IEnumerable<Order> GetOrdersByYear(int year)
        {
            var yearParameter = new SqlParameter(StoredProceduresConstants.YearParameter, year);
            var result = _appContext.Orders.FromSqlRaw($"{StoredProceduresConstants.GetOrdersByYear} {StoredProceduresConstants.YearParameter}", yearParameter);
            return result;
        }

        public void Update(Order entity)
        {
            var item = _appContext.Orders.Find(entity.Id);
            _appContext.Entry(item).CurrentValues.SetValues(entity);
            _appContext.SaveChanges();
        }
    }
}
