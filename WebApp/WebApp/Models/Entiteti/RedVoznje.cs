using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models.Entiteti
{
    public class RedVoznje
    {
        public int Id { get; set; }

        public Linija LinijaId { get; set; }

        public Linija Linija { get; set; }
    }
}