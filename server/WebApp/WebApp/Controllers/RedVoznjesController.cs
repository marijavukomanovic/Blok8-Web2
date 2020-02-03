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
    [RoutePrefix("api/RedVoznje")]
    public class RedVoznjesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private IRedVoznjeRepository redVoznjeRepository;
        private ILinijaRepository linijaRepository;
        private ITipLinijeRepository tipLinijeRepository;
        private ITipDanaRepository tipDanaRepository;

        private static readonly Object lockObj = new Object();
        public RedVoznjesController(ILinijaRepository linijaRepository, ITipLinijeRepository tipLinijeRepository, ITipDanaRepository tipDanaRepository, IRedVoznjeRepository redVoznjeRepository)
        {
            this.redVoznjeRepository = redVoznjeRepository;
            this.tipDanaRepository = tipDanaRepository;
            this.linijaRepository = linijaRepository;
            this.tipLinijeRepository = tipLinijeRepository;
        }

        [ResponseType(typeof(List<string>))]
        [Route("GetLinije/{type}")]//vrati linije za odredjeni tip lnija
        public IHttpActionResult GetLines1(int type)
        {
            List<string> retVal = new List<string>();
            string s = " ";
            bool tipPostoji = false;
            if (linijaRepository.GetAll().Count() == 0)
            { return BadRequest("Nema nijedne linije"); }
            foreach (var i in linijaRepository.GetAll())
            {
                if (i.Aktivna == true)
                {
                    if (i.TipId == type)
                    {
                        tipPostoji = true;

                        s += i.RedBroj.ToString();

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

        [AllowAnonymous]//valjda moze i admin i user
        [ResponseType(typeof(string))]
        [Route("GetRedVoznje/{tipDana}/{linija}")]//u odnosu na to koja je linija i koji je dan vrati se red voznje
        public IHttpActionResult GetSchedule(int tipDana, string linija)
        {
            lock (lockObj)

            {
                string redVoznje = "";
                int lineId = -1;

                //if (linijaRepository.GetAll().Count() == 0) nama ne popunja va lepo linije bazu pa me je strah da ne udje ovde
                //{
                //    return BadRequest("Ne postoji nijedna linija");
                //}
                foreach (var l in linijaRepository.GetAll())
                {
                    if (l.RedBroj.Trim(' ').Equals(linija.Trim(' ')) && l.Aktivna)
                    {
                        lineId = l.Id;
                        break;
                    }
                }
                if (redVoznjeRepository.GetAll().Count() == 0)
                {
                    return BadRequest("Josuvek ne postoji ni jedan red voznje za bilo koji liniju niti tip dana");
                }
                foreach (var s in redVoznjeRepository.GetAll())
                {
                    if (s.LinijaId == lineId && s.TipDanaId == tipDana && s.Aktivan==true)//proverava da li je trazena linija, da li je odgovaradjuci dan
                    {
                        redVoznje += s.RasporedVoznje;
                        break;
                    }

                }
                if (redVoznje.Trim(' ').Equals(" "))
                    redVoznje = "Ne postoji red voznej za odabranu liniju i dan molim vas dodaj te novi";



                return Ok(redVoznje);
            }
        }

        //[AllowAnonymous]
        [Authorize(Roles = "Admin")]
        [Route("GetRedVoznjeNovi/{tipDana}/{linija}/{stringInfo}")]
        public IHttpActionResult GetNewSchedule(int tipDana, string linija,string stringInfo)
        {
            lock (lockObj)

            {
                
                RedVoznje redVoznje = new RedVoznje();
                if (redVoznjeRepository.GetAll().Count() == 0)
                {
                    return Ok("Ne postoji red voznje dodaj te ga");
                }

                int idLinije = -1;
                foreach (var l in linijaRepository.GetAll())
                {
                    if (l.RedBroj.Trim(' ').Equals(linija.Trim(' ')) && l.Aktivna)
                    {
                        idLinije = l.Id;
                        break;
                    }
                }
                foreach (var redV in redVoznjeRepository.GetAll())
                {
                    if (redV.LinijaId == idLinije && redV.TipDanaId == tipDana &&redV.Aktivan==true)
                    {
                        redV.RasporedVoznje = stringInfo.ToString();
                        db.Entry(redV).State = EntityState.Modified;
                        db.SaveChanges();
                        break;
                    }
                    else
                    {
                        int id = redVoznjeRepository.GetAll().Count() + 1;
                        redVoznje.LinijaId = idLinije;
                        redVoznje.TipDanaId = tipDana;
                        redVoznje.RasporedVoznje = stringInfo;
                        redV.Aktivan = true;

                        db.RedoviVoznje.Add(redVoznje);
                        db.SaveChanges();
                        break;
                    }


                }

                return Ok();
            }
        }

        //[AllowAnonymous]
        [Authorize(Roles ="Admin")]
        [Route("ObrisiRedVoznje/{tipDana}/{linija}")]
        public IHttpActionResult ObrisiRedVoznje(int tipDana, string linija)
        {
            lock (lockObj)
            {

                int idLinije = -1;
                foreach (var l in linijaRepository.GetAll())
                {
                    if (l.RedBroj.Trim(' ').Equals(linija.Trim(' ')) && l.Aktivna)
                    {
                        idLinije = l.Id;
                        break;
                    }
                }
                foreach (var redV in redVoznjeRepository.GetAll())
                {
                    if (redV.LinijaId == idLinije && redV.TipDanaId == tipDana && redV.Aktivan == true)
                    {
                        redV.Aktivan = false;

                        break;
                    }
                }

                return Ok();
            }
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