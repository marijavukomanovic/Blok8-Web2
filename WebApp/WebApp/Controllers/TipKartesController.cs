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

namespace WebApp.Controllers
{
    public class TipKartesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/TipKartes
        public IQueryable<TipKarte> GetTipKarte()
        {
            return db.TipKarte;
        }

        // GET: api/TipKartes/5
        [ResponseType(typeof(TipKarte))]
        public IHttpActionResult GetTipKarte(int id)
        {
            TipKarte tipKarte = db.TipKarte.Find(id);
            if (tipKarte == null)
            {
                return NotFound();
            }

            return Ok(tipKarte);
        }

        // PUT: api/TipKartes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTipKarte(int id, TipKarte tipKarte)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tipKarte.Id)
            {
                return BadRequest();
            }

            db.Entry(tipKarte).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipKarteExists(id))
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

        // POST: api/TipKartes
        [ResponseType(typeof(TipKarte))]
        public IHttpActionResult PostTipKarte(TipKarte tipKarte)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.TipKarte.Add(tipKarte);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tipKarte.Id }, tipKarte);
        }

        // DELETE: api/TipKartes/5
        [ResponseType(typeof(TipKarte))]
        public IHttpActionResult DeleteTipKarte(int id)
        {
            TipKarte tipKarte = db.TipKarte.Find(id);
            if (tipKarte == null)
            {
                return NotFound();
            }

            db.TipKarte.Remove(tipKarte);
            db.SaveChanges();

            return Ok(tipKarte);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TipKarteExists(int id)
        {
            return db.TipKarte.Count(e => e.Id == id) > 0;
        }
    }
}