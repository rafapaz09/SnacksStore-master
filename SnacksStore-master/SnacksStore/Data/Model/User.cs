using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SnacksStore.Data.Model
{
    public class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "username is required.")]
        public string Username { get; set; }

        [Required]
        [JsonIgnore]
        public string Password { get; set; }

        [JsonIgnore]
        public string PasswordSalt { get; set; }

        public bool? Active { get; set; }
        public DateTime? CreatedAt { get; set; }

        [JsonIgnore]
        public int RolId { get; set; }

        public virtual Rol Rol { get; set; }

        [NotMapped]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Token { get; set; }
    }
}
