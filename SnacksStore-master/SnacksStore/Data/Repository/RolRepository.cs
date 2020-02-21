﻿using Microsoft.EntityFrameworkCore;
using SnacksStore.Data.Interfaces;
using SnacksStore.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SnacksStore.Data.Repository
{
    public class RolRepository : Repository<Rol>, IRolRepository
    {
        public RolRepository(SnacksStoreContext context) :base(context)
        {

        }

    }
}
