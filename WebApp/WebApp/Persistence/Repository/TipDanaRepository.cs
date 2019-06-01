using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApp.Models.Entiteti;

namespace WebApp.Persistence.Repository
{
    public class TipDanaRepository : Repository<TipDana, int>, ITipDanaRepository
    {
        public TipDanaRepository(DbContext context) : base(context)
        {
        }

    }
}