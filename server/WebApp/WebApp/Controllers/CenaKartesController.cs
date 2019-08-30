using System;
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
            DateTime date = new DateTime();
            int id = cenovnikRepository.GetAll().Count();
            if (id == 0)//ako je baza prazna ne postoji nijedan cenovnik, onda nam je pocetni datum trenutni
            { date = DateTime.Now; }
            date = cenovnikRepository.Get(id).VazenjeDo;//za sledeci cenovnik pocetni datum je krajnji datum prethodnog vazeceg
            date = new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second+1);
            retVal = date.ToLongDateString() + " " + date.ToLongTimeString();

            return Ok(retVal);
        }

        //kontroler za pravljenje novog cenovnika iz angulara mi saljes sve podatke
        //[Authorize(Roles = "Admin")]
        [AllowAnonymous]
        [System.Web.Http.HttpPost]
        [Route("Cenovnik/{model}")]
        public async Task<IHttpActionResult> Cenovnik(CenovnikBindingModel model)//pravi novi cenovnik
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
            Cenovnik noviCenovnik = new Cenovnik() { VazenjeOd = DateTime.Parse(model.OD), VazenjeDo = DateTime.Parse(model.DO), Id = ++idc,Aktivan=true, };
            db.Cenovnici.Add(noviCenovnik);
            db.SaveChanges();
            int idck = CenaKarteRepository.GetAll().Count();

            //  idck++;
            for (int i = 1; i <= 4; i++)
            {
                CenaKarte cenaKarte = new CenaKarte() { Id = ++idck, Cena = cene[i - 1], CenovnikId = idc, TipKarteId = i,Aktivan=true, };
                db.CenaKarata.Add(cenaKarte);
                db.SaveChanges();
            }


            return Ok();
        }

        //[Authorize(Roles = "Admin")]
        [AllowAnonymous]
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
                return BadRequest("Ne postoji ni jedan cenovnik");
            }
            if (CenaKarteRepository.GetAll().Count() == 0)//nikada ne bi trebalo da se dogodi
            {
                return BadRequest("Ne postoje cene,jos uvek nisu zadate");
            }
            foreach (var tCenovnik in cenovnikRepository.GetAll())
            {
                if (DateTime.Compare(tCenovnik.VazenjeOd, DateTime.Now) < 0 &&
                       DateTime.Compare(tCenovnik.VazenjeDo, DateTime.Now) > 0 &&tCenovnik.Aktivan==true)
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
                OD = (new DateTime(t1.Year, t1.Month, t1.Day, t1.Hour, t1.Minute, t1.Second)).ToString(),
                DO = (new DateTime(t2.Year, t2.Month, t2.Day, 23, 59, 59)).ToString(),
                cenaDnevna = cene[1],
                cenaVremenska = cene[0],
                cenaMesecna = cene[2],
                cenaGodisnja = cene[3],
            };

            return Ok(retVal);
        }

        [AllowAnonymous]
        //[Authorize(Roles = "Admin")]
        [System.Web.Http.HttpPost]
        [Route("IzmenaCenovnika2/{model}")]
        public async Task<IHttpActionResult> IzmenaCenovnika2(CenovnikBindingModel model)//cuvanje u bazi izmenu cenovnika
        {
            int idCenovnika = -1;
            int idCenaKarte = -1;
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
                    tCenovnik.Aktivan = false;

                }

            }
            foreach (var ck in CenaKarteRepository.GetAll())
            {
                if (ck.CenovnikId == idCenovnika)
                {
                    idCenaKarte = ck.Id;
                    ck.Aktivan = false;
                    break;
                }
            }

            int idc = cenovnikRepository.GetAll().Count();


            double[] cene = new double[] { model.cenaVremenska, model.cenaDnevna, model.cenaMesecna, model.cenaGodisnja };

            for (int i = 0; i < 4; i++)//provera dali je cena ispravni tj veca od 0
            {
                if (cene[i] < 0)
                    cene[i] = 0;

            }
            //provera da li je vazenje vece od pocetaka vazenja cenovnika
            if (!(DateTime.Compare(DateTime.Parse(model.OD), DateTime.Parse(model.DO)) < 0))//ako je vrenjeDo manje od vazenjaOd vrati gresku
            { return BadRequest("Niste zadali odgovarajuci datum, datum trajanja ne mozze biti manji od datuma pocetka "); }
            Cenovnik noviCenovnik = new Cenovnik() { VazenjeOd = DateTime.Parse(model.OD), VazenjeDo = DateTime.Parse(model.DO), Id = ++idc, Aktivan = true, };
            db.Cenovnici.Add(noviCenovnik);
            db.SaveChanges();
            int idck = CenaKarteRepository.GetAll().Count();

            //  idck++;

            for (int i = 1; i <= 4; i++)
            {
                CenaKarte cenaKarte = new CenaKarte() { Id = ++idck, Cena = cene[i - 1], CenovnikId = idc, TipKarteId = i, Aktivan = true, };
                db.CenaKarata.Add(cenaKarte);
                db.SaveChanges();
            }
            CenovnikBindingModel izmenjeniCenovnik = new CenovnikBindingModel()
            {
                OD = noviCenovnik.VazenjeOd.ToLongDateString() + " " + noviCenovnik.VazenjeOd.ToLongTimeString(),
                DO = noviCenovnik.VazenjeDo.ToLongDateString() + " " + noviCenovnik.VazenjeDo.ToLongTimeString(),


            };

            foreach (var ck in CenaKarteRepository.GetAll())
            {if (idCenaKarte == ck.Id)
                {
                    if (ck.TipKarteId == 1)
                    { izmenjeniCenovnik.cenaVremenska = ck.Cena; }
                    if (ck.TipKarteId == 2)
                    { izmenjeniCenovnik.cenaDnevna = ck.Cena; }
                    if (ck.TipKarteId == 3)
                    { izmenjeniCenovnik.cenaMesecna = ck.Cena; }
                    if (ck.TipKarteId == 4)
                    { izmenjeniCenovnik.cenaGodisnja = ck.Cena; }
                }

            }
            

            

            return Ok();
        }


        // GET: api/CenaKarte/GetCena
        [AllowAnonymous]//[Authorize(Roles ="AppUser")]//valjda
        [System.Web.Http.HttpGet]
        [Route("GetCena/{type}/{username}")]
        [ResponseType(typeof(double))]
        public async Task<IHttpActionResult> GetCena(int type, string username)//u ondaosu na tip dana vraca cenu, treba vratiti i username da bi nasla u bazi kog je tipa user zbog koegicienta
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

        [AllowAnonymous]
        [System.Web.Http.HttpGet]
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
                    break;
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
        [System.Web.Http.HttpGet]
        [Route("IzlistajMojeKarte/{username}")]
        [ResponseType(typeof(List<UserTicketBindingModel>))]
        public async Task<IHttpActionResult> IzlistajMojeKarte(string username)
        {
            List<UserTicketBindingModel> userTicketBindingModels = new List<UserTicketBindingModel>();

            DateTime vremeIzdavanje = new DateTime();
            DateTime vremeTrajanja = new DateTime();
            int idKarte = -1;
            int idCeneKarte = -1;
            string tipKarte = "";
            int tiiipKarte = -1;
            int idKorisnika = -1;
            int tipPutnika = -1;
            double koef = 0;
            double cena = 0;
            foreach (var korisnik in korisnikRepository.GetAll())
            {
                if (korisnik.KorisnickoIme.Equals(username))
                {
                    idKorisnika = korisnik.Id;
                    tipPutnika = korisnik.TipId;
                    break;

                }
            }
            koef = tipPutnikaRepository.Get(tipPutnika).Koeficijent;


            foreach (var karta in kartaRepository.GetAll())//ako je korisnik kupio kartu
            {
                if (karta.ApplicationUserId.Equals(idKorisnika.ToString()))
                {
                    idKarte = karta.Id;
                    idCeneKarte = karta.CenaKarteId;
                    vremeIzdavanje = karta.VremeKupovine;


                }

                tiiipKarte = CenaKarteRepository.Get(idCeneKarte).TipKarteId;
                cena = CenaKarteRepository.Get(idCeneKarte).Cena;

                switch (tiiipKarte)
                {
                    case 1:
                        vremeTrajanja = vremeIzdavanje.AddHours(1);
                        tipKarte = "Vremenska";
                        break;
                    case 2:
                        if (vremeIzdavanje.Month % 2 == 0 && vremeIzdavanje.Month != 2 && vremeIzdavanje.Month != 8)
                            vremeTrajanja = new DateTime(vremeIzdavanje.Year, vremeIzdavanje.Month, 30, 23, 59, 59);
                        if (vremeIzdavanje.Month == 2)
                        {
                            if (vremeIzdavanje.Year % 4 == 0)
                                vremeTrajanja = new DateTime(vremeIzdavanje.Year, vremeIzdavanje.Month, 29, 23, 59, 59);
                            else { vremeTrajanja = new DateTime(vremeIzdavanje.Year, vremeIzdavanje.Month, 28, 23, 59, 59); }
                        }
                        else { vremeTrajanja = new DateTime(vremeIzdavanje.Year, vremeIzdavanje.Month, 31, 23, 59, 59); }
                        tipKarte = "Dnevna";
                        break;
                    case 3:
                        vremeTrajanja = new DateTime(vremeIzdavanje.Year, vremeIzdavanje.Month, vremeIzdavanje.Day, 23, 59, 59);
                        tipKarte = "Mesecna";
                        break;
                    case 4:
                        vremeTrajanja = new DateTime(vremeIzdavanje.Year, 12, 31); //oni su na vrezama rekli da traje do kraja godine nebitno kada se izda
                        tipKarte = "Godisnja";
                        break;
                }
                UserTicketBindingModel ticketBindingModel = new UserTicketBindingModel(idKarte.ToString(), tipKarte, vremeIzdavanje.ToLongDateString() + " " + vremeIzdavanje.ToLongTimeString(), vremeTrajanja.ToLongDateString() + " " + vremeTrajanja.ToLongTimeString(), cena * koef);


                userTicketBindingModels.Add(ticketBindingModel);
            }
            return Ok(userTicketBindingModels);
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