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
using WebApp.Models.Entiteti;
using WebApp.Persistence;
using WebApp.Persistence.Repository;

namespace WebApp.Controllers
{[RoutePrefix("api/RedVoznje")]
    public class RedVoznjesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private IRedVoznjeRepository redVoznjeRepository;
        private ILinijaRepository linijaRepository;
        private ITipLinijeRepository tipLinijeRepository;
        private ITipDanaRepository tipDanaRepository;
        public RedVoznjesController(ILinijaRepository linijaRepository, ITipLinijeRepository tipLinijeRepository,ITipDanaRepository tipDanaRepository, IRedVoznjeRepository redVoznjeRepository)
        {
            this.redVoznjeRepository = redVoznjeRepository;
            this.tipDanaRepository = tipDanaRepository;
            this.linijaRepository = linijaRepository;
            this.tipLinijeRepository = tipLinijeRepository;
        }
        [ResponseType(typeof(List<string>))]
        [Route("GetLinije/{type}")]
        public IHttpActionResult GetLines1(int type)
        {
            List<string> retVal = new List<string>();
            string s = " ";

            foreach (var i in linijaRepository.GetAll())
            {
                if (i.Aktivna == true)
                {
                    if (i.TipId == type)
                    {

                        s += i.RedBroj.ToString();
                        // s += i.Stanice; ne odajemo u liniji koje ima stanice za sad
                        retVal.Add(s);
                        s = "";
                    }
                }
            }
            return Ok(retVal);
        }
        [ResponseType(typeof(string))]
        [Route("GetRedVoznje/{tipDana}/{linija}")]
        public IHttpActionResult GetSchedule( int tipDana, string linija)
        {
            string redVoznje = "";
            int lineI = -1;
            char[] str = new char[] { '-' };
            string[] lineName = linija.Split(str);

            foreach (var l in linijaRepository.GetAll())
            {
                if (l.RedBroj == int.Parse(lineName[0]))
                {
                    lineI = l.RedBroj;
                }
            }
            foreach (var s in redVoznjeRepository.GetAll())
            {
                if (s.LinijaId == lineI && s.TipDanaId == tipDana )//proverava da li je trazena linija, da li je odgovaradjuci dan
                {
                    redVoznje += " " + s.RasporedVoznje;
                }
            }

            return Ok(redVoznje);
        }





        // GET: api/RedVoznjes
        public IQueryable<RedVoznje> GetRedoviVoznje()
        {
            return db.RedoviVoznje;
        }

        // GET: api/RedVoznjes/5
        [ResponseType(typeof(RedVoznje))]
        public IHttpActionResult GetRedVoznje(int id)
        {
            RedVoznje redVoznje = db.RedoviVoznje.Find(id);
            if (redVoznje == null)
            {
                return NotFound();
            }

            return Ok(redVoznje);
        }

        // PUT: api/RedVoznjes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRedVoznje(int id, RedVoznje redVoznje)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != redVoznje.Id)
            {
                return BadRequest();
            }

            db.Entry(redVoznje).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RedVoznjeExists(id))
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

        // POST: api/RedVoznjes
        [ResponseType(typeof(RedVoznje))]
        public IHttpActionResult PostRedVoznje(RedVoznje redVoznje)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.RedoviVoznje.Add(redVoznje);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = redVoznje.Id }, redVoznje);
        }

        // DELETE: api/RedVoznjes/5
        [ResponseType(typeof(RedVoznje))]
        public IHttpActionResult DeleteRedVoznje(int id)
        {
            RedVoznje redVoznje = db.RedoviVoznje.Find(id);
            if (redVoznje == null)
            {
                return NotFound();
            }

            db.RedoviVoznje.Remove(redVoznje);
            db.SaveChanges();

            return Ok(redVoznje);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RedVoznjeExists(int id)
        {
            return db.RedoviVoznje.Count(e => e.Id == id) > 0;
        }
    }
}