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
        public TipPutnika Tip { get; set; }
        public byte[] Document { get; set; }
        [ForeignKey("Status")]
        public int StatusId { get; set; }
        public Status Status { get; set; }
       

        public Korisnik() { }
    }
}