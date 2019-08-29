﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using WebApp.Models;
using WebApp.Persistence;
using WebApp.Persistence.Repository;

namespace WebApp.Controllers
{
    [RoutePrefix("api/CenaKarte")]
    public class CenaKartesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private ICenaKarteRepository CenaKarteRepository;
        private ICenovnikRepository cenovnikRepository;
        private IKartaRepository kartaRepository;
        private IKorisnikRepository korisnikRepository;
        private ITipPutnikaRepository tipPutnikaRepository;

        public CenaKartesController(ICenaKarteRepository CenaKarteRepository, ICenovnikRepository cenovnikRepository, IKartaRepository kartaRepository, IKorisnikRepository korisnikRepository, ITipPutnikaRepository TipPutnikaRepository)
        // GET: api/CenaKartes
        {
            this.tipPutnikaRepository = TipPutnikaRepository;
            this.kartaRepository = kartaRepository;
            this.korisnikRepository = korisnikRepository;
            this.cenovnikRepository = cenovnikRepository;
            this.CenaKarteRepository = CenaKarteRepository;
        }
        public IQueryable<CenaKarte> GetCenaKarata()
        {
            return db.CenaKarata;
        }
        //kontroler za slanje poslednjeg datuma
        // GET: api/StationLine/GetLines
        [Authorize(Roles = "Admin")]
        [System.Web.Http.HttpGet]
        [Route("GetPoslednjiDatum")]
        [ResponseType(typeof(string))]
        public IHttpActionResult GetPoslednjiDatum()
        {
            string retVal = "";
            DateTime date;
            int id = cenovnikRepository.GetAll().Count();
            if (id == 0)//ako je baza prazna ne postoji nijedan cenovnik, onda nam je pocetni datum trenutni
            { date = DateTime.Now; }
            date = cenovnikRepository.Get(id).VazenjeDo;//za sledeci cenovnik pocetni datum je krajnji datum prethodnog vazeceg
            date = date.AddDays(1);//date=date.AddHours(12); ako ima idalje onog probelma o cemu smo pricale
            retVal = date.ToShortDateString().ToString();

            return Ok(retVal);
        }

        //kontroler za pravljenje novog cenovnika iz angulara mi saljes sve podatke
        //[Authorize(Roles = "Admin")]
        [AllowAnonymous]
        [System.Web.Http.HttpPost]
        [Route("Cenovnik/{model}")]
        public async Task<IHttpActionResult> Cenovnik(CenovnikBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // var userStore = new UserStore<ApplicationUser>(db);
            //var userManager = new UserManager<ApplicationUser>(userStore);
            int idc = cenovnikRepository.GetAll().Count();


            double[] cene = new double[] { model.cenaVremenska, model.cenaDnevna, model.cenaMesecna, model.cenaGodisnja };

            for (int i = 0; i < 4; i++)//provera dali je cena ispravni tj veca od 0
            {
                if (cene[i] < 0)
                    cene[i] = 0;
                //ili mozda da vratim gresku pa da korisnik mora ponovo da zada???//Marina ?
                //{return BadRequest("Cena ne moze biti manja od nule ");} i u if-u da bude <=

            }
            //provera da li je vazenje vece od pocetaka vazenja cenovnika
            if (!(DateTime.Compare(DateTime.Parse(model.OD), DateTime.Parse(model.DO)) < 0))//ako je vrenjeDo manje od vazenjaOd vrati gresku
            { return BadRequest("Niste zadali odgovarajuci datum, datum trajanja ne mozze biti manji od datuma pocetka "); }
            Cenovnik noviCenovnik = new Cenovnik() { VazenjeOd = DateTime.Parse(model.OD), VazenjeDo = DateTime.Parse(model.DO), Id = ++idc, };
            db.Cenovnici.Add(noviCenovnik);
            db.SaveChanges();
            int idck = CenaKarteRepository.GetAll().Count();

            //  idck++;
            for (int i = 1; i <= 4; i++)
            {
                CenaKarte cenaKarte = new CenaKarte() { Id = ++idck, Cena = cene[i - 1], CenovnikId = idc, TipKarteId = i };
                db.CenaKarata.Add(cenaKarte);
                db.SaveChanges();
            }


            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [System.Web.Http.HttpGet]
        [ResponseType(typeof(CenovnikBindingModel))]
        [Route("IzmenaCenovnika")]
        public async Task<IHttpActionResult> IzmenaCenovnika()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            DateTime t1 = new DateTime();
            DateTime t2 = new DateTime();
            int idCenovnika = -1;
            if (cenovnikRepository.GetAll().Count() == 0)//
            {
                return BadRequest("Ne postoji ni jedn cenovnik");
            }
            if (CenaKarteRepository.GetAll().Count() == 0)//nikada ne bi trebalo da se dogodi
            {
                return BadRequest("Ne postoje cene,josuvek nisu zadate");
            }
            foreach (var tCenovnik in cenovnikRepository.GetAll())
            {
                if (DateTime.Compare(tCenovnik.VazenjeOd, DateTime.Now) < 0 &&
                       DateTime.Compare(tCenovnik.VazenjeDo, DateTime.Now) > 0)
                {
                    t1 = tCenovnik.VazenjeOd;
                    t2 = tCenovnik.VazenjeDo;
                    idCenovnika = tCenovnik.Id;
                }

            }
            double[] cene = new double[4];
            foreach (var tcenakarte in CenaKarteRepository.GetAll())
            {
                if (tcenakarte.CenovnikId == idCenovnika)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        if (tcenakarte.TipKarteId == i + 1)
                            cene[i] = tcenakarte.Cena;
                    }
                }
            }
            CenovnikBindingModel retVal = new CenovnikBindingModel()
            {
                OD = t1.ToShortDateString(),
                DO = t2.ToShortDateString(),
                cenaDnevna = cene[1],
                cenaVremenska = cene[0],
                cenaMesecna = cene[2],
                cenaGodisnja = cene[3],
            };

            return Ok(retVal);
        }
        [Authorize(Roles = "Admin")]
        [System.Web.Http.HttpPost]
        [Route("IzmenaCenovnika2/{model}")]
        public async Task<IHttpActionResult> IzmenaCenovnika2(CenovnikBindingModel model)
        {
            int idCenovnika = -1;
            int idCenaKarte = -1;
            bool provera = false;
            DateTime t1 = DateTime.Parse(model.OD);
            DateTime t2 = DateTime.Parse(model.DO);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!(DateTime.Compare(t1, t2) < 0))
            { return BadRequest("Niste zadali validno trajanje cenovnika"); }


            foreach (var tCenovnik in cenovnikRepository.GetAll())
            {
                if (DateTime.Compare(tCenovnik.VazenjeOd, t1) == 0 &&
                       DateTime.Compare(tCenovnik.VazenjeDo, t2) == 0)
                {
                    idCenovnika = tCenovnik.Id;
                }

            }
            foreach (var ck in CenaKarteRepository.GetAll())
            {
                if (ck.CenovnikId == idCenovnika)
                {
                    idCenaKarte = ck.Id;
                    if (ck.TipKarteId == 1)
                    {
                        if (model.cenaDnevna <= 0)//provera validane vrednosti cene karte
                        { return BadRequest("Losa cena dnevne karte"); }
                        else
                        {
                            ck.Cena = model.cenaVremenska; provera = true;
                        }
                    }
                    else if (ck.TipKarteId == 2)
                    {
                        if (model.cenaVremenska <= 0)//provera validane vrednosti cene karte
                        {
                            return BadRequest("Losa cena vremenske karte");
                        }
                        else
                        { ck.Cena = model.cenaDnevna; provera = true; }
                    }
                    else if (ck.TipKarteId == 3)
                    {
                        if (model.cenaMesecna <= 0)//provera validane vrednosti cene karte
                        {
                            return BadRequest("Losa cena mesecne karte");
                        }
                        else
                        { ck.Cena = model.cenaMesecna; provera = true; }
                    }
                    else if (ck.TipKarteId == 4)
                    {
                        if (model.cenaGodisnja <= 0)//provera validane vrednosti cene karte
                        {
                            return BadRequest("Losa cena godisnje karte");
                        }
                        else
                        { ck.Cena = model.cenaGodisnja; provera = true; }
                    }

                }

                if (provera)
                {
                    db.Entry(ck).State = EntityState.Modified;
                    db.SaveChanges();
                    provera = false;
                }

            }


            return Ok();
        }

        [AllowAnonymous]
        // [System.Web.Http.HttpGet]
        [Route("KupiKartu/{tipKarte}/{username}")]
        public async Task<IHttpActionResult> KupiKartu(int tipKarte, string username)
        {
            int idKorisnika = -1;
            int tipKorisnika = -1;
            int idKarte = kartaRepository.GetAll().Count();

            foreach (var korisnici in korisnikRepository.GetAll())
            {
                if (korisnici.KorisnickoIme.Equals(username))
                {
                    idKorisnika = korisnici.Id;
                    tipKorisnika = korisnici.TipId;
                }
            }
            Karta novaKarta = new Karta();
            novaKarta.Id = ++idKarte;
            novaKarta.ApplicationUserId = idKorisnika.ToString();
            foreach (var c in CenaKarteRepository.GetAll())
            {
                if (c.TipKarteId == tipKarte)
                {
                    novaKarta.CenaKarteId = c.Id;
                    break;
                }
            }
            novaKarta.VremeKupovine = DateTime.Now;
            db.Karte.Add(novaKarta);
            db.SaveChanges();



            return Ok();
        }

        [AllowAnonymous]
         [System.Web.Http.HttpPost]
        [Route("IzlizstajMojeKarte/{username}")]
        [ResponseType(typeof(List<UserTicketBindingModel>))]
        public async Task<IHttpActionResult> IzlizstajMojeKarte(string username)
        {
            List<UserTicketBindingModel> userTicketBindingModels = new List<UserTicketBindingModel>();
            UserTicketBindingModel ticketBindingModel = new UserTicketBindingModel();
            DateTime vremeIzdavanje=new DateTime();
            DateTime vremeTrajanja=new DateTime();
            int idKarte = -1;
            int idCeneKarte = -1;
            string tipKarte = "";
            int tiiipKarte = -1;
            int idKorisnika = -1;
            foreach (var korisnik in korisnikRepository.GetAll())
            {if (korisnik.Email == username)
                {
                    idKorisnika = korisnik.Id;
                    break;

                }
            }
            foreach (var karta in kartaRepository.GetAll())//ako je korisnik kupio kartu
            {
                if (karta.ApplicationUserId.Equals(idKorisnika.ToString()))
                {
                    idKarte = karta.Id;
                    idCeneKarte = karta.CenaKarteId;//odavce cu uzeti koji je tip
                    vremeIzdavanje = karta.VremeKupovine;
                }


                foreach (var ck in CenaKarteRepository.GetAll())//pronajdi kog je tipa ona bila
                {
                    if (ck.Id == idCeneKarte)
                    {
                        tipKarte = ck.TipKarte.Naziv;
                        tiiipKarte = ck.TipKarteId;
                    }

                }
                switch (tipKarte)
                {
                    case "Vremenska":
                        vremeTrajanja = vremeIzdavanje.AddHours(1);
                        break;
                    case "Dnevna":
                        vremeTrajanja = vremeIzdavanje.AddDays(1);
                        break;
                    case "Mesecna":
                        vremeTrajanja = vremeIzdavanje.AddMonths(1);
                        break;
                    case "Godisnja":
                        vremeTrajanja = new DateTime(vremeIzdavanje.Year, 12, 31); //oni su na vrezama rekli da traje do kraja godine nebitno kada se izda
                        break;
                }

                ticketBindingModel.TicketId = idKarte.ToString();
                ticketBindingModel.TicketType = tipKarte;
                ticketBindingModel.IssuingTime = vremeIzdavanje.ToShortDateString();
                ticketBindingModel.ExpirationTime = vremeTrajanja.ToShortDateString();
                userTicketBindingModels.Add(ticketBindingModel);
            }


            return Ok(userTicketBindingModels);
        }

        // GET: api/CenaKarte/GetCena
        [AllowAnonymous]//[Authorize(Roles ="AppUser")]//valjda
        [System.Web.Http.HttpGet]
        [Route("GetCena/{type}/{username}")]//Marina treba mi jos i username iz localStorage
        [ResponseType(typeof(double))]//dodat koeficient
        public IHttpActionResult GetLines(int type, string username)//u ondaosu na tip dana vraca cenu, treba vratiti i username da bi nasla u bazi kog je tipa user zbog koegicienta
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
                }

            }
            foreach (var korisnik in korisnikRepository.GetAll())
            {
                if (korisnik.KorisnickoIme.Equals(username))
                    tipKorisnika = korisnik.TipId;
            }

            if (CenaKarteRepository.GetAll().Count() == 0)
            { return BadRequest("Ne postoji cene karte, molim vas napravite ih"); }

            foreach (var tipp in tipPutnikaRepository.GetAll())
            {
                if (tipp.Id == tipKorisnika)
                {
                    koeficient = tipp.Koeficijent;
                    break;
                }
            }
            foreach (var ck in CenaKarteRepository.GetAll())
            {
                if (ck.CenovnikId == idCenovnika && ck.TipKarteId == type)
                    retCena = ck.Cena*koeficient;//treba ce se dodati i koeficijet ovde u odnosu na kog je tipa korisnik

            }


            return Ok(retCena);
        }

        //ostali su izgenerisani

        // GET: api/CenaKartes/5
        [ResponseType(typeof(CenaKarte))]
        public IHttpActionResult GetCenaKarte(int id)
        {
            CenaKarte cenaKarte = db.CenaKarata.Find(id);
            if (cenaKarte == null)
            {
                return NotFound();
            }

            return Ok(cenaKarte);
        }

        // PUT: api/CenaKartes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCenaKarte(int id, CenaKarte cenaKarte)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cenaKarte.Id)
            {
                return BadRequest();
            }

            db.Entry(cenaKarte).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CenaKarteExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/CenaKartes
        [ResponseType(typeof(CenaKarte))]
        public IHttpActionResult PostCenaKarte(CenaKarte cenaKarte)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CenaKarata.Add(cenaKarte);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = cenaKarte.Id }, cenaKarte);
        }

        // DELETE: api/CenaKartes/5
        [ResponseType(typeof(CenaKarte))]
        public IHttpActionResult DeleteCenaKarte(int id)
        {
            CenaKarte cenaKarte = db.CenaKarata.Find(id);
            if (cenaKarte == null)
            {
                return NotFound();
            }

            db.CenaKarata.Remove(cenaKarte);
            db.SaveChanges();

            return Ok(cenaKarte);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CenaKarteExists(int id)
        {
            return db.CenaKarata.Count(e => e.Id == id) > 0;
        }
    }
}