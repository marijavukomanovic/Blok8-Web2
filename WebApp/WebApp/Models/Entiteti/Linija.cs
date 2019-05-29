using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class Linija
    {
        [Key]
        public int RedBroj { get; set; }
        public List<Stanica> Stanice { get; set; }
    }
}