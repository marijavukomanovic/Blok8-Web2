using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class Autobus
    {
        public int Id { get; set; }
        public int BrojLinijeId { get; set; }//fk za broj linije da bi mogao odmah da nadje(akoje 1:n)
        public Linija BrojLinije { get; set; }//automacki uveze sa prethodnim BrojLinijeId za to je bitno da se zovu isto samo da jedan ima id,ako hocemo da se zoovu razliciti treba dodati [fk] iznad jednog i unutar fk pod "" dodamo naziv onig drugog
        public double GeografskeKoordinataX { get; set; }
        public double GeografskeKoordinataY { get; set; }
        //trenutna poz
    }
}