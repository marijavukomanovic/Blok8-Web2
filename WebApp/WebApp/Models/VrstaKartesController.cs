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

namespace WebApp.Models
{
    public class VrstaKartesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/VrstaKartes
        public IQueryable<VrstaKarte> GetVrstaKarte()
        {
            return db.VrstaKarte;
        }

        // GET: api/VrstaKartes/5
        [ResponseType(typeof(VrstaKarte))]
        public IHttpActionResult GetVrstaKarte(int id)
        {
            VrstaKarte vrstaKarte = db.VrstaKarte.Find(id);
            if (vrstaKarte == null)
            {
                return NotFound();
            }

            return Ok(vrstaKarte);
        }

        // PUT: api/VrstaKartes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutVrstaKarte(int id, VrstaKarte vrstaKarte)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != vrstaKarte.Id)
            {
                return BadRequest();
            }

            db.Entry(vrstaKarte).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VrstaKarteExists(id))
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

        // POST: api/VrstaKartes
        [ResponseType(typeof(VrstaKarte))]
        public IHttpActionResult PostVrstaKarte(VrstaKarte vrstaKarte)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.VrstaKarte.Add(vrstaKarte);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = vrstaKarte.Id }, vrstaKarte);
        }

        // DELETE: api/VrstaKartes/5
        [ResponseType(typeof(VrstaKarte))]
        public IHttpActionResult DeleteVrstaKarte(int id)
        {
            VrstaKarte vrstaKarte = db.VrstaKarte.Find(id);
            if (vrstaKarte == null)
            {
                return NotFound();
            }

            db.VrstaKarte.Remove(vrstaKarte);
            db.SaveChanges();

            return Ok(vrstaKarte);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool VrstaKarteExists(int id)
        {
            return db.VrstaKarte.Count(e => e.Id == id) > 0;
        }
    }
}