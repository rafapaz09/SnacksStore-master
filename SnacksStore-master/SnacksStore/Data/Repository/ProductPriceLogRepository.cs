using SnacksStore.Data.Interfaces;
using SnacksStore.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SnacksStore.Data.Repository
{
    public class ProductPriceLogRepository : Repository<ProductPriceLog>, IProductPriceLogRepository
    {
        public ProductPriceLogRepository(SnacksStoreContext context) : base(context)
        {
        }
    }
}
