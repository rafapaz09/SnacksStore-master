using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SnacksStore.Helpers
{
    public class SortableAttribute : Attribute
    {
        public string OrderBy { get; set; }
    }
}
