using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SnacksStore.Data.Model
{
    public class Rol
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool? Active { get; set; }
        public DateTime? CreatedAt { get; set; }

    }
}
