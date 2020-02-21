using Newtonsoft.Json;
using SnacksStore.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SnacksStore.Data.Model
{
    public class Product
    {
        public int Id { get; set; }

        public string Sku { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal? Price { get; set; }

        public int? Stock { get; set; }

        public int? Likes { get; set; }
        public bool? Active { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public int CreatedBy { get; set; }
        [ForeignKey("CreatedBy")]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]

        public virtual User CreatedByUser { get; set; }

        public int? UpdatedBy { get; set; }

        [ForeignKey("UpdatedBy")]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]

        public virtual User UpdatedByUser { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public ICollection<ProductPriceLog> PriceLogs { get; set; }
    }
}
