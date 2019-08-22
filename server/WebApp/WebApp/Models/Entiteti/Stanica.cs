using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class Stanica
    {
        [Key]
        public String Naziv { get; set; }
        public String Adresa { get; set; }
        public double GeografskeKoordinataX { get; set; }
        public double GeografskeKoordinataY { get; set; }
        //public List<Linija> Linije { get; set; }
        public bool Aktivna { get; set; } // za logicko brisanje

    }
}