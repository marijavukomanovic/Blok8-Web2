using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Models.Entiteti;

namespace WebApp.Models
{
    
    public class Karta
    {
        public int Id { get; set; }
        public VrstaKarte Naziv { get; set; }

        public DateTime PocetakVazenja { get; set; }
        public DateTime ZavrsetakVazenja { get; set; }


       
    }
}