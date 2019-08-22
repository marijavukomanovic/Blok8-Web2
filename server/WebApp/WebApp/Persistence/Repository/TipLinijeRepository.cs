using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApp.Models.Entiteti;

namespace WebApp.Persistence.Repository
{
    public class TipLinijeRepository : Repository<TipLinije, int>, ITipLinijeRepository
    {
        public TipLinijeRepository(DbContext context) : base(context)
        {
        }
    }
}