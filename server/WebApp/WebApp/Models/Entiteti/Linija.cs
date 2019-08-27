using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using WebApp.Models.Entiteti;

namespace WebApp.Models
{
    public class Linija
    {  [Key]
        public int Id { get; set; }
        public string RedBroj { get; set; }
     //   public List<Stanica> Stanice { get; set; }

        [ForeignKey("Tip")]
        public int TipId { get; set; }
        public TipLinije Tip { get; set; }
        public string Opis { get; set; }
        public string Boja { get; set; }

        public bool Aktivna { get; set; } // za logicko brisanje 
    }
}