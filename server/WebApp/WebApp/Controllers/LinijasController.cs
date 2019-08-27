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
        private IStanicaRepository stanionRepository;

        private static readonly Object lockObject = new Object();
        public LinijasController(ILinijaRepository il, IStanicaRepository stanicaRepository)
        {
            this.lineRepo = il;
            this.stanionRepository = stanicaRepository;
        }

        // GET: api/StationLine/GetLines
        [AllowAnonymous]
        [System.Web.Http.HttpGet]
        [Route("GetLines/{type}")]
        [ResponseType(typeof(List<string>))]
        public IHttpActionResult GetLines(int type)//vrati koje su linije za odredjeni tip (gradski,prigratski)
        {
            List<string> retVal = new List<string>();
            string s = " ";
            bool tipPostoji = false;
            if (lineRepo.GetAll().Count() == 0)
            { return BadRequest("Nema nijedne linije"); }
            foreach (var i in lineRepo.GetAll())
            {
                if (i.Aktivna == true)
                {
                    if (i.TipId == type)
                    {
                        tipPostoji = true;
                        s += i.RedBroj.ToString();
                        // s += i.Stanice; ne odajemo u liniji koje ima stanice za sad
                        retVal.Add(s);
                        s = "";
                    }
                }
            }
            if (tipPostoji == false)
            {
                return BadRequest("Za trazeni tip ne postoji nijedna linija");//ili mozda jos bolje da vratim u retval poruku ? Marina ? 
            }
            return Ok(retVal);
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