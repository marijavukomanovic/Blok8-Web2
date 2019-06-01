using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using WebApp.Models.Entiteti;

namespace WebApp.Models
{
    public class CenaKarte
    {
        public int Id { get; set; }

        [ForeignKey("Cenovnik")]
        public int CenovnikId { get; set; }
        public Cenovnik Cenovnik { get; set; }

        [ForeignKey("TipKarte")]
        public int TipKarteId { get; set; }
        public TipKarte TipKarte { get; set; }

        public double Cena { get; set; }
    }
}