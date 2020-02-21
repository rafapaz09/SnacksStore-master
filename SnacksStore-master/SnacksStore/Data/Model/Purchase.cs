using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SnacksStore.Data.Model
{
    public class Purchase
    {
        public int Id { get; set; }
        public int NumberOfProducts { get; set; }
        public decimal Total { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public int ClientId { get; set; }

        [ForeignKey("ClientId")]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public virtual User Client { get; set; }

        public int CreatedBy { get; set; }
        [ForeignKey("CreatedBy")]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]

        public virtual User CreatedByUser { get; set; }

        public int? UpdatedBy { get; set; }
        [ForeignKey("UpdatedBy")]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]

        public virtual User UpdatedByUser { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public ICollection<PurchaseProducts> Products { get; set; }
    }
}
