using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApp.Models.Entiteti;

namespace WebApp.Persistence.Repository
{
    public class StatusRepository:Repository<Status, int>,IStatusRepository
    {
        public StatusRepository(DbContext context) : base(context)
    {
    }
}
}