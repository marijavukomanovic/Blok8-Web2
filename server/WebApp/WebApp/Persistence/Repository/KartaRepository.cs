using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApp.Models;

namespace WebApp.Persistence.Repository
{
    public class KartaRepository : Repository<Karta, int>, IKartaRepository
    {
        public KartaRepository(DbContext context) : base(context)
        {
        }
    }
}