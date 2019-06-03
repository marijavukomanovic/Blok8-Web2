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
    public class TipDanasController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/TipDanas
        public IQueryable<TipDana> GetTipDana()
        {
            return db.TipDana;
        }

        // GET: api/TipDanas/5
        [ResponseType(typeof(TipDana))]
        public IHttpActionResult GetTipDana(int id)
        {
            TipDana tipDana = db.TipDana.Find(id);
            if (tipDana == null)
            {
                return NotFound();
            }

            return Ok(tipDana);
        }

        // PUT: api/TipDanas/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTipDana(int id, TipDana tipDana)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tipDana.Id)
            {
                return BadRequest();
            }

            db.Entry(tipDana).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipDanaExists(id))
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

        // POST: api/TipDanas
        [ResponseType(typeof(TipDana))]
        public IHttpActionResult PostTipDana(TipDana tipDana)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.TipDana.Add(tipDana);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tipDana.Id }, tipDana);
        }

        // DELETE: api/TipDanas/5
        [ResponseType(typeof(TipDana))]
        public IHttpActionResult DeleteTipDana(int id)
        {
            TipDana tipDana = db.TipDana.Find(id);
            if (tipDana == null)
            {
                return NotFound();
            }

            db.TipDana.Remove(tipDana);
            db.SaveChanges();

            return Ok(tipDana);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TipDanaExists(int id)
        {
            return db.TipDana.Count(e => e.Id == id) > 0;
        }
    }
}