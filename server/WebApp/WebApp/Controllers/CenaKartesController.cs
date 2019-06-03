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

namespace WebApp.Controllers
{
    public class CenaKartesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/CenaKartes
        public IQueryable<CenaKarte> GetCenaKarata()
        {
            return db.CenaKarata;
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