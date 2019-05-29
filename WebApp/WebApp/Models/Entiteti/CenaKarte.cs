using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Models.Entiteti;

namespace WebApp.Models
{
    public class CenaKarte
    {
        public int Id { get; set; }
        public int CenovnikId { get; set; }
        public Cenovnik Cenovnik { get; set; }
        public VrstaKarte VrstaKarte { get; set; }
    }
}