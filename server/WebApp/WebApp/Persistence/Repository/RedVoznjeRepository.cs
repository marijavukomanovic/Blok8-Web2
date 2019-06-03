using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApp.Models;
using WebApp.Models.Entiteti;

namespace WebApp.Persistence.Repository
{
    public class RedVoznjeRepository : Repository<RedVoznje, int>, IRedVoznjeRepository
    {
        public RedVoznjeRepository(DbContext context) : base(context)
        {
        }
    }
}