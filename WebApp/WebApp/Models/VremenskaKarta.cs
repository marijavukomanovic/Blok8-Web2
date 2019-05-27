using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class VremenskaKarta
    {
        public int Id { get; set; }
        public DateTime VremeCekiranja { get; set; }
        public double Cena { get; set; }
    }
}