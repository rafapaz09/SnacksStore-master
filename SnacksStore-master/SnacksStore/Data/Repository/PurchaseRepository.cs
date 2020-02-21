using Microsoft.EntityFrameworkCore;
using SnacksStore.Data.Interfaces;
using SnacksStore.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SnacksStore.Data.Repository
{
    public class PurchaseRepository : Repository<Purchase>, IPurchaseRepository
    {
        public PurchaseRepository(SnacksStoreContext context) : base(context)
        {
        }

        public Purchase GetByIdWithProducts(int id)
        {
            return _context.Purchases.Where(p => p.Id == id)
                .Include(detail => detail.Products)
                    .ThenInclude(product => product.Product)
                .First();
        }
    }
}
