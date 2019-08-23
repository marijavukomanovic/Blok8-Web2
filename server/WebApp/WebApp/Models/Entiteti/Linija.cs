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
    {
        [Key]
        public int RedBroj { get; set; }
        public ICollection<Stanica> Stanice { get; set; }

        [ForeignKey("Tip")]
        public int TipId { get; set; }
        public TipLinije Tip { get; set; }

        public bool Aktivna { get; set; } // za logicko brisanje 
    }
}