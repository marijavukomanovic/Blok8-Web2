using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApp.Models;
using WebApp.Models.Entiteti;

namespace WebApp.Persistence.Repository
{
    public class TipKarteRepository : Repository<TipKarte, int>, ITipKarteRepository
    {
        public TipKarteRepository(DbContext context) : base(context)
        {
        }
    }
}