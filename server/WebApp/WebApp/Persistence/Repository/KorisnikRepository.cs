using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApp.Models.Entiteti;

namespace WebApp.Persistence.Repository
{
    public class KorisnikRepository : Repository<Korisnik, int>, IKorisnikRepository
    {
        public KorisnikRepository(DbContext context) : base(context)
        {
        }
    }
}
