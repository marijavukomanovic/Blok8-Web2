using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class Autobus
    {
        public int Id { get; set; }
        public Linija BrojLinije { get; set; }
        public int Pozicija { get; set; }
    }
}