using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApp.Models;
using WebApp.Models.Entiteti;

namespace WebApp.Persistence.Repository
{
    public class VrstaKarteRepository : Repository<VrstaKarte, int>, IVrstaKarteRepository
    {
        public VrstaKarteRepository(DbContext context) : base(context)
        {
        }
    }
}