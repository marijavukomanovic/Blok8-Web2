using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.Models.Entiteti
{
    public class TipPutnika
    {
        [Key]
        public int Id { get; set; }
        public String Naziv { get; set; }
        public Double Koeficijent { get; set; }
    }
}