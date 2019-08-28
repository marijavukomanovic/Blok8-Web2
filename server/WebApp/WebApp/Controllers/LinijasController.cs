using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebApp.Models;
using WebApp.Models.Entiteti;
using WebApp.Persistence;
using WebApp.Persistence.Repository;

namespace WebApp.Controllers
{
    [RoutePrefix("api/Linije")]
    public class LinijasController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private ILinijaRepository lineRepo;
        private IStanicaRepository stationRepository;
        private ILinijeStaniceRepository linijeStaniceRepository;
        private ITipLinijeRepository tipLinijeRepository;

        private static readonly Object lockObject = new Object();
        public LinijasController(ILinijaRepository il, IStanicaRepository stanicaRepository, ILinijeStaniceRepository st, ITipLinijeRepository tip)
        {
            tipLinijeRepository = tip;
            this.lineRepo = il;
            this.stationRepository = stanicaRepository;
            this.linijeStaniceRepository = st;
        }

        [AllowAnonymous]
        [Route("CreateLineStations/{model}")]
        public IHttpActionResult CreateLineStations(LineStBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (lineRepo.Find(x => x.RedBroj.Equals(model.LineId)).Count() != 0)
            {
                return BadRequest("Vec ste postoji linija sa takvim imenom");
            }
            int idLinije = -1;
            int tipLinije = -1;
            int brojStanica = model.Stations.Count();
            int idStanica = -1;
            int idLinijaStanica = -1;

            switch (model.LineType)
            {
                case "Gradski":
                    tipLinije = 1;
                    break;
                case "Prigradski":
                    tipLinije = 2;
                    break;

            }
            idLinije = lineRepo.GetAll().Count();
            Linija novaLinija = new Linija()
            {
                Id = ++idLinije,
                RedBroj = model.LineId,
                TipId = tipLinije,
                Tip = tipLinijeRepository.Get(tipLinije),
                Opis = model.Description,
                Boja = model.Color,
            };
            db.Linije.Add(novaLinija);
            db.SaveChanges();

            
            for (int i = 0; i < brojStanica; i++)
            {
                LinijeStanice novaLinijaStanice = new LinijeStanice();
                idLinijaStanica = linijeStaniceRepository.GetAll().Count();
                idStanica = stationRepository.GetAll().Count();
                Stanica novaStanica = new Stanica()
                {
                    Id = ++idStanica,
                    Naziv = model.Stations[i].Name,
                    Adresa = model.Stations[i].Address,
                    GeografskeKoordinataX = model.Stations[i].XCoordinate,
                    GeografskeKoordinataY = model.Stations[i].YCoordinate

                };
                novaLinijaStanice.Id = ++idLinijaStanica;
                novaLinijaStanice.LinijeId = idLinije;
               // novaLinijaStanice.Linije = novaLinija;
                novaLinijaStanice.StaniceId = novaStanica.Id;
               // novaLinijaStanice.Stanice = novaStanica;

                db.Stanice.Add(novaStanica);
                db.SaveChanges();

                db.LinijeStanices.Add(novaLinijaStanice);
                db.SaveChanges();
            }
            return Ok();
        }


        [AllowAnonymous]
        [System.Web.Http.HttpGet]
        [Route("GetLineName/{type}")]
        [ResponseType(typeof(List<string>))]
        public IHttpActionResult GetLineName(int type)//vrati koje su linije za odredjeni tip (gradski,prigratski)
        {
            List<string> linijeSveZaTiip = new List<string>();
            foreach (var linije in lineRepo.GetAll())
            {
                if (linije.TipId == type)
                {
                    if (lineRepo.GetAll().Count() == 0)
                    { return BadRequest("Ne postoje linije za trazeni tip"); }

                    linijeSveZaTiip.Add(linije.RedBroj);
                }
            }

            return Ok(linijeSveZaTiip);
        }

        // GET: api/StationLine/GetLines
        [AllowAnonymous]
        [System.Web.Http.HttpGet]
        [Route("GetLines/{linijaIme}")]
        [ResponseType(typeof(LineStBindingModel))]
        public IHttpActionResult GetLines(string linijaIme)//vrati koje su linije za odredjeni tip (gradski,prigratski)
        {
            LineStBindingModel retval = new LineStBindingModel();
            LineStBindingModel lineStBindingModel = new LineStBindingModel();
            List<StationBindingModel> stations = new List<StationBindingModel>();
            int linijeId = -1;
            foreach (var linija in lineRepo.GetAll())
            {
                if (linija.RedBroj.Equals(linijaIme))
                {
                    linijeId = linija.Id;
                    break;
                }

            }
            if (linijeId == -1)//nikad ane bi trebalo da se dogodi jer mi nudimo linije
            {
                return BadRequest("Trazena linija ne postoji");
            }

            foreach (var st1 in linijeStaniceRepository.GetAll())
            {
                if (st1.LinijeId == linijeId)
                {
                    StationBindingModel s = new StationBindingModel();
                    foreach (var s1 in stationRepository.GetAll())
                    {if (st1.StaniceId == s1.Id)
                        {
                            s.Name = s1.Naziv;
                            s.Address = s1.Adresa;
                            s.XCoordinate = s1.GeografskeKoordinataX;
                            s.YCoordinate = s1.GeografskeKoordinataY;
                            stations.Add(s);
                            break;
                        }

                    }

                    //s.Name = stationRepository.Get(st1.StaniceId.ToString()).Naziv;
                    //s.Address = stationRepository.Get(st1.StaniceId.ToString()).Adresa;
                    //s.XCoordinate = stationRepository.Get(st1.StaniceId.ToString()).GeografskeKoordinataX;
                    //s.YCoordinate = stationRepository.Get(st1.StaniceId.ToString()).GeografskeKoordinataY;
                    //lineStBindingModel.Stations.Add(s);
                }
            }
            lineStBindingModel.LineId = linijaIme;
            lineStBindingModel.Description = lineRepo.Get(linijeId).Opis;//"Linija " + linijaIme + "ima" + lineStBindingModel.Stations.Count() + "stanica";
            lineStBindingModel.Color = lineRepo.Get(linijeId).Boja;
            lineStBindingModel.LineType = tipLinijeRepository.Get(linijeId).Naziv;
            lineStBindingModel.Stations=stations;
            retval=lineStBindingModel;

            return Ok(retval);


        }
        // GET: api/Linijas
        public IQueryable<Linija> GetLinije()
        {
            return db.Linije;

        }

        // GET: api/Linijas/5
        [ResponseType(typeof(Linija))]
        public IHttpActionResult GetLinija(int id)
        {
            Linija linija = db.Linije.Find(id);
            if (linija == null)
            {
                return NotFound();
            }

            return Ok(linija);
        }

        // PUT: api/Linijas/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutLinija(int id, Linija linija)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != linija.Id)
            {
                return BadRequest();
            }

            db.Entry(linija).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LinijaExists(id))
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

        // POST: api/Linijas
        [ResponseType(typeof(Linija))]
        public IHttpActionResult PostLinija(Linija linija)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Linije.Add(linija);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = linija.Id }, linija);
        }

        // DELETE: api/Linijas/5
        [ResponseType(typeof(Linija))]
        public IHttpActionResult DeleteLinija(int id)
        {
            Linija linija = db.Linije.Find(id);
            if (linija == null)
            {
                return NotFound();
            }

            db.Linije.Remove(linija);
            db.SaveChanges();

            return Ok(linija);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LinijaExists(int id)
        {
            return db.Linije.Count(e => e.Id == id) > 0;
        }
    }
}