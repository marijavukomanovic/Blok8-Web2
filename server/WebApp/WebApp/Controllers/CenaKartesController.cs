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

        public CenaKartesController(ICenaKarteRepository CenaKarteRepository, ICenovnikRepository cenovnikRepository)
        // GET: api/CenaKartes
        {
            this.cenovnikRepository = cenovnikRepository;
            this.CenaKarteRepository = CenaKarteRepository;
        }
        public IQueryable<CenaKarte> GetCenaKarata()
        {
            return db.CenaKarata;
        }
        //kontroler za slanje poslednjeg datuma
        // GET: api/StationLine/GetLines
        [AllowAnonymous]
        [System.Web.Http.HttpGet]
        [Route("GetPoslednjiDatum")]
        [ResponseType(typeof(string))]
        public IHttpActionResult GetPoslednjiDatum()
        {
            string retVal = "";
            DateTime date;
            int id = cenovnikRepository.GetAll().Count();
            date = cenovnikRepository.Get(id).VazenjeDo;
            date = date.AddDays(1);
            retVal = date.ToShortDateString().ToString();

            return Ok(retVal);
        }

        //kontroler za pravljenje novog cenovnika iz angulara mi saljes sve podatke
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
        [AllowAnonymous]
        [System.Web.Http.HttpPost]
        [Route("IzmenaCenovnika2/{model}")]
        public async Task<IHttpActionResult> IzmenaCenovnika2(CenovnikBindingModel model)
        {
            int idCenovnika = -1;
            int idCenaKarte = -1;
            bool provera = false;
            DateTime t1 =   DateTime.Parse(model.OD);
            DateTime t2 = DateTime.Parse(model.DO);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
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
                    { ck.Cena = model.cenaVremenska; provera = true; }
                    else if (ck.TipKarteId == 2)
                    { ck.Cena = model.cenaDnevna; provera = true; }
                    else if (ck.TipKarteId == 3)
                    { ck.Cena = model.cenaMesecna; provera = true; }
                    else if (ck.TipKarteId == 4)
                    { ck.Cena = model.cenaGodisnja; provera = true; }
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

        // GET: api/CenaKarte/GetCena
        [AllowAnonymous]
        [System.Web.Http.HttpGet]
        [Route("GetCena/{type}")]
        [ResponseType(typeof(double))]
        public IHttpActionResult GetLines(int type)
        {
            double retCena = -1;
            int idCenovnika = -1;//cenovnika koji jos uvek vazi
            foreach (var c in cenovnikRepository.GetAll())
            {
                if (DateTime.Compare(c.VazenjeDo, DateTime.Now) > 0)
                {
                    idCenovnika = c.Id;
                }

            }
            foreach (var ck in CenaKarteRepository.GetAll())
            {
                if (ck.CenovnikId == idCenovnika && ck.TipKarteId == type)
                    retCena = ck.Cena;//treba ce se dodati i koeficijet ovde u odnosu na kog je tipa korisnik

            }


            return Ok(retCena);
        }



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