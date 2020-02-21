using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using SnacksStore.Data.Model;

namespace SnacksStore.Data.Interfaces
{
    public interface IProductRepository : IRepository<Product> , IPagination<Product>
    {
        IEnumerable<Product> FindWithCreatedBy(Func<Product, bool> predicate);
        IEnumerable<Product> GetByIdWithProductPriceLog(int id);

        void AddLike(Product entity, int quantity);

        IQueryable<Product> GetAllAvailable(string search);

        bool CheckQuantityAvailable(int id, int quantity);

    }
}
