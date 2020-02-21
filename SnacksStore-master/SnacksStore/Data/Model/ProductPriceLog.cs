using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SnacksStore.Data.Model
{
    public class ProductPriceLog
    {
        public int Id { get; set; }
        
        public decimal OldPrice { get; set; }
        public decimal NewPrice { get; set; }
        public DateTime CreatedAt { get; set; }

        [JsonIgnore]
        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }

        public int CreatedBy { get; set; }

        [ForeignKey("CreatedBy")]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]

        public virtual User CreatedByUser { get; set; }


    }
}
