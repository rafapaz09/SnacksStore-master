using SnacksStore.Data.Interfaces;
using SnacksStore.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SnacksStore.Data.Repository
{
    public class PurchaseProductRepository : Repository<PurchaseProducts>, IPurchaseProductRepository
    {
        public PurchaseProductRepository(SnacksStoreContext context) : base(context)
        {
        }
    }
}
