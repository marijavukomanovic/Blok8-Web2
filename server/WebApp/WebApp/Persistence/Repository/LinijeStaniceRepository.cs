using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApp.Models.Entiteti;

namespace WebApp.Persistence.Repository
{
    public class LinijeStaniceRepository: Repository<LinijeStanice, int>, ILinijeStaniceRepository
    {
        public LinijeStaniceRepository(DbContext context) : base(context)
        {
        }
    }
   
}