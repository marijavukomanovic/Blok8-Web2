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

        private static readonly Object lockObject = new Object();
        public LinijasController(ILinijaRepository il, IStanicaRepository stanicaRepository,ILinijeStaniceRepository st)
        {
            this.lineRepo = il;
            this.stationRepository = stanicaRepository;
            this.linijeStaniceRepository = st;
        }

        // GET: api/StationLine/GetLines
        [AllowAnonymous]
        [System.Web.Http.HttpGet]
        [Route("GetLines")]
        [ResponseType(typeof(StationBindingModel))]
        public IHttpActionResult GetLines()//vrati koje su linije za odredjeni tip (gradski,prigratski)
        {
            StationBindingModel st = new StationBindingModel();
            foreach (var st1 in stationRepository.GetAll())
            {
                st.Address = st1.Adresa;
                st.Name = st1.Naziv;
                st.XCoordinate = st1.GeografskeKoordinataX;
                st.YCoordinate = st1.GeografskeKoordinataY;
            }

            return Ok(st);

            // LineStBindingModel
            // StationBindingModel

            /*List<LineStBindingModel> retVal = new List<LineStBindingModel>();
            
            bool tipPostoji = false;
            List<int> idLinija = new List<int>();
            string tip ="";
            if (type == 1)
            {
                tip = "Gradski";
            }
            else if(type == 2)
            {
                tip = "Prigradski";
            }
            
            if (lineRepo.GetAll().Count() == 0)
            {
                return BadRequest("Nema nijedne linije");
            }

            //id-evi svih linija koje su zadatog tipa
            foreach (var i in lineRepo.GetAll())
            {
                if (i.Aktivna == true)
                {
                    if (i.TipId == type)
                    {
                        tipPostoji = true;
                        idLinija.Add(i.Id);
                        LineStBindingModel ret = new LineStBindingModel();
                        ret.LineId = i.Id.ToString();
                        ret.LineType = tip;
                        ret.Color = i.Boja;
                        ret.Description = i.Opis;
                        ret.Stations = new List<StationBindingModel>();
                        retVal.Add(ret);
                    }
                }
            }
            for (int i=0;i<idLinija.Count;i++)
            {
                foreach(var lin in linijeStaniceRepository.GetAll())
                {
                    if(lin.LinijeId == idLinija[i])
                    {
                        int idSt = lin.StaniceId;
                        foreach(var sta in stationRepository.GetAll())
                        {
                            if (idSt == sta.Id)
                            {
                                foreach(var p in retVal)
                                {
                                    if (Equals(p.LineId,idSt.ToString()))
                                    {
                                        StationBindingModel stbm = new StationBindingModel();
                                            stbm.Address = sta.Adresa;
                                        stbm.Name = sta.Naziv;
                                        stbm.IsStation = "";
                                        stbm.XCoordinate = sta.GeografskeKoordinataX;
                                        stbm.YCoordinate = sta.GeografskeKoordinataY;
                                        p.Stations.Add(stbm);
                                    }
                                }
                            }
                        }
                    }
                }

            }
            if (tipPostoji == false)
            {
                return BadRequest("Za trazeni tip ne postoji nijedna linija");//ili mozda jos bolje da vratim u retval poruku ? Marina ? 
            }
            return Ok(retVal);*/
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