using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class Autobus
    {
        public int Id { get; set; }
        [ForeignKey("Linija")]
        public int LinijaId { get; set; }//fk za broj linije da bi mogao odmah da nadje(akoje 1:n)
        public Linija Linija { get; set; }//automacki uveze sa prethodnim BrojLinijeId za to je bitno da se zovu isto samo da jedan ima id,ako hocemo da se zoovu razliciti treba dodati [fk] iznad jednog i unutar fk pod "" dodamo naziv onig drugog

        //trenutna poz
    }
}