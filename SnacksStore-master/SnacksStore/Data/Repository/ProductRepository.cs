using Microsoft.EntityFrameworkCore;
using SnacksStore.Data.Interfaces;
using SnacksStore.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SnacksStore.Data.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository, IPagination<Product>
    {
        public ProductRepository(SnacksStoreContext context) : base(context)
        {
        }

        public void AddLike(Product entity, int quantity)
        {
            entity.Likes = (entity.Likes ?? 0) + quantity;
            Update(entity);
        }

        public IEnumerable<Product> GetByIdWithProductPriceLog(int id)
        {
            return _context.Products.Where(p => p.Id == id).Include(p => p.PriceLogs).ToList();
        }

        public IEnumerable<Product> FindWithCreatedBy(Func<Product, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Product> GetAllAvailable(string search)
        {
            if(string.IsNullOrWhiteSpace(search))
                return _context.Products.Where(p => (bool)p.Active && p.Stock > 0).AsQueryable();
            else
                return _context.Products.Where(p => (bool)p.Active && p.Stock > 0 && p.Name.Contains(search)).AsQueryable();
        }

        public bool CheckQuantityAvailable(int id, int quantity)
        {
            return (_context.Products.Where(
                p => p.Id == id && (bool)p.Active && p.Stock >= quantity
                ).Count() > 0);
        }

        public IQueryable<Product> GetPaginated(string filter, int initialPage, int pageSize, string order, out int totalRecords, out int recordsFiltered)
        {
            var data = _context.Products.AsQueryable();
            totalRecords = data.Count();

            if (!string.IsNullOrEmpty(filter))
            {
                data = data.Where(x => x.Name.ToUpper().Contains(filter.ToUpper()));
            }

            recordsFiltered = data.Count();

            if(order.ToUpper().Equals("ASC"))
                data = data.OrderBy(x => x.Likes)
                        .ThenBy(x => x.Name)
                        .Skip((initialPage * pageSize))
                        .Take(pageSize);
            else
                data = data.OrderByDescending(x => x.Likes)
                        .ThenBy(x => x.Name)
                        .Skip((initialPage * pageSize))
                        .Take(pageSize);

            return data;
        }
    }
}
