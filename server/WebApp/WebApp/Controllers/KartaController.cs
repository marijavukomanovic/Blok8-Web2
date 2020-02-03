using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebApp.Models;
using WebApp.Persistence;
using WebApp.Persistence.Repository;

namespace WebApp.Controllers
{
    [RoutePrefix("api/Karta")]
    public class KartaController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private ICenaKarteRepository CenaKarteRepository;
        private ICenovnikRepository cenovnikRepository;
        private IKartaRepository kartaRepository;
        private IKorisnikRepository korisnikRepository;
        private ITipPutnikaRepository tipPutnikaRepository;
        //private IPayPalRepository payPalRepository;
        private static readonly Object lockObj = new Object();

        public KartaController(ICenaKarteRepository CenaKarteRepository, ICenovnikRepository cenovnikRepository, IKartaRepository kartaRepository, IKorisnikRepository korisnikRepository, ITipPutnikaRepository TipPutnikaRepository/*, IPayPalRepository PayPalRepository*/)
        // GET: api/CenaKartes
        {
            this.tipPutnikaRepository = TipPutnikaRepository;
            this.kartaRepository = kartaRepository;
            this.korisnikRepository = korisnikRepository;
            this.cenovnikRepository = cenovnikRepository;
            this.CenaKarteRepository = CenaKarteRepository;
            //this.payPalRepository = PayPalRepository;
        }

        [AllowAnonymous]//[Authorize(Roles ="AppUser")]//valjda        //[System.Web.Http.HttpGet]
        [ResponseType(typeof(double))]
        [Route("GetCena/{type}/{username}")]
        public IHttpActionResult GetCena(int type, string username)//u ondaosu na tip dana vraca cenu, treba vratiti i username da bi nasla u bazi kog je tipa user zbog koegicienta
        {

            double retCena = -1;
            double koeficient = 0;
            int tipKorisnika = -1;
            int idCenovnika = -1;//cenovnika koji jos uvek vazi
            if (cenovnikRepository.GetAll().Count() == 0)
            { return BadRequest("Ne postoji ni jedan cenovnik, molim vas napravite ga"); }
            foreach (var c in cenovnikRepository.GetAll())
            {
                if (DateTime.Compare(c.VazenjeDo, DateTime.Now) > 0)
                {
                    idCenovnika = c.Id;
                    break;
                }

            }

            if (username.Equals("admin") || username.Equals("appu"))
            {
                retCena = CenaKarteRepository.Get(type).Cena;

                return Ok(retCena);
            }


            foreach (var korisnik in korisnikRepository.GetAll())
            {

                if (korisnik.KorisnickoIme.Equals(username))
                {
                    tipKorisnika = korisnik.TipId;
                    break;
                }
            }

            if (CenaKarteRepository.GetAll().Count() == 0)
            { return BadRequest("Ne postoji cene karte, molim vas napravite ih"); }


            koeficient = tipPutnikaRepository.Get(tipKorisnika).Koeficijent;
            foreach (var ck in CenaKarteRepository.GetAll())
            {
                if (ck.CenovnikId == idCenovnika && ck.TipKarteId == type)
                {
                    retCena = ck.Cena * koeficient;//*koeficient;//treba ce se dodati i koeficijet ovde u odnosu na kog je tipa korisnik
                    break;
                }
            }


            return Ok(retCena);
        }

    }
}
