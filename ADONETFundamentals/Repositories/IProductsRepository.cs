using ADONETFundamentals.Models;
using System.Collections.Generic;

namespace ADONETFundamentals.Repositories
{
    public interface IProductsRepository
    {
        public int Create(Product entity);

        public void Update(Product entity);

        public Product GetById(int id);

        public void Delete(int id);

        public IEnumerable<Product> GetAll();
    }
}
