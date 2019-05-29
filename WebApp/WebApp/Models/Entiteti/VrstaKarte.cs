using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApp.Models.Entiteti
{
    public class VrstaKarte
    {
        public int Id { get; set; }

        //[ForeignKey("TipKarte")]
        public int TipKarteId { get; set; }
        public TipKarte TipKarte { get; set; }


    }
}