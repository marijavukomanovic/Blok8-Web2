using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApp.Models.Entiteti
{
    public class RedVoznje
    {
        public int Id { get; set; }
        public string RasporedVoznje { get; set; }

        [ForeignKey("Linija")]
        public int LinijaId { get; set; }
        public Linija Linija { get; set; }

        [ForeignKey("TipDana")]
        public int TipDanaId { get; set; }
        public TipDana TipDana { get; set; }
        public bool Aktivan { get; set; }
    }
}