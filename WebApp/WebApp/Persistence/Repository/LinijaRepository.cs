using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApp.Models;

namespace WebApp.Persistence.Repository
{
    public class LinijaRepository : Repository<Linija, int>, ILinijaRepository
    {
        public LinijaRepository(DbContext context) : base(context)
        {
        }
    }
}