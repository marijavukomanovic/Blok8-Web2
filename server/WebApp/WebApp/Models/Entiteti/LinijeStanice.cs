using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApp.Models.Entiteti
{
    public class LinijeStanice
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Linije")]
        public int LinijeId { get; set; }
        public Linija Linije { get; set; }

        [ForeignKey("Stanice")]
        public int StaniceId { get; set; }
        public Stanica Stanice { get; set; }
    }
}