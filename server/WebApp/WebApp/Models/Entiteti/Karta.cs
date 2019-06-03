using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using WebApp.Models.Entiteti;

namespace WebApp.Models
{
    
    public class Karta
    {
        public int Id { get; set; }

        [ForeignKey("CenaKarte")]
        public int CenaKarteId { get; set; }
        public CenaKarte CenaKarte { get; set; }

        [ForeignKey("ApplicationUser")]
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public DateTime VremeKupovine { get; set; }



    }
}