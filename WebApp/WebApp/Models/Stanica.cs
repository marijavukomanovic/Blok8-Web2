using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class Stanica
    { [Key]
        public String Naziv { get; set; }
        public String Adresa { get; set; }
        public int GeografskeKoordinate { get; set; }
        public List<Linija> Linije { get; set; }

    }
}