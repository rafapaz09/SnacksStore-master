using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SnacksStore.Data.Model
{
    public class PurchaseProducts
    {
        public int Id { get; set; }
        public int ProductQuantity { get; set; }
        public decimal Price { get; set; }
        public decimal SubTotal { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        [JsonIgnore]
        public int PurchaseId { get; set; }

        [ForeignKey("PurchaseId")]
        public virtual Purchase Purchase { get; set; }


        [JsonIgnore]
        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }


        public int CreatedBy { get; set; }

        [ForeignKey("CreatedBy")]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public virtual User CreatedByUser { get; set; }


        public int? UpdatedBy { get; set; }

        [ForeignKey("UpdatedBy")]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public virtual User UpdatedByUser { get; set; }

        

    }
}
