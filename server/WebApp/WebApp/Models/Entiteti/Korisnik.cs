using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApp.Models.Entiteti
{
    public class Korisnik
    {
        public int Id { get; set; }
        public string KorisnickoIme { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Email { get; set; }
        public string Sifra { get; set; }
        public DateTime DatumRodjenja { get; set; }
        public string Adresa { get; set; }
        [ForeignKey("Tip")]
        public int TipId { get; set; }
        //public TipPutnika TipPutnika { get; set; }
        public TipPutnika Tip { get; set; }

        public Korisnik() { }
    }
}