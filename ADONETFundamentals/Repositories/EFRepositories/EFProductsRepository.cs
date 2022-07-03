using ADONETFundamentals.Models;
using System.Collections.Generic;

namespace ADONETFundamentals.Repositories.EFRepositories
{
    public class EFProductsRepository : IProductsRepository
    {
        private readonly ApplicationContext _appContext;

        public EFProductsRepository(ApplicationContext appContext)
        {
            _appContext = appContext;
        }

        public int Create(Product entity)
        {
            _appContext.Products.Add(entity);
            _appContext.SaveChanges();
            return entity.Id;
        }

        public void Delete(int id)
        {
            var entity = _appContext.Products.Find(id);
            _appContext.Products.Remove(entity);
            _appContext.SaveChanges();
        }

        public IEnumerable<Product> GetAll()
        {
            var query = _appContext.Products;
            return query;
        }

        public Product GetById(int id)
        {
            var entity = _appContext.Products.Find(id);
            return entity;
        }

        public void Update(Product entity)
        {
            var item = _appContext.Products.Find(entity.Id);
            _appContext.Entry(item).CurrentValues.SetValues(entity);
            _appContext.SaveChanges();
        }
    }
}
