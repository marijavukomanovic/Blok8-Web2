﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApp.Models;

namespace WebApp.Persistence.Repository
{
    public class CenaKarteRepository : Repository<CenaKarte, int>, ICenaKarteRepository
    {
        public CenaKarteRepository(DbContext context) : base(context)
        {
        }
    }
}