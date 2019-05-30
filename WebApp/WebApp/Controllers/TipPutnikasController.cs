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
    public class TipPutnikasController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/TipPutnikas
        public IQueryable<TipPutnika> GetTipPutnika()
        {
            return db.TipPutnika;
        }

        // GET: api/TipPutnikas/5
        [ResponseType(typeof(TipPutnika))]
        public IHttpActionResult GetTipPutnika(int id)
        {
            TipPutnika tipPutnika = db.TipPutnika.Find(id);
            if (tipPutnika == null)
            {
                return NotFound();
            }

            return Ok(tipPutnika);
        }

        // PUT: api/TipPutnikas/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTipPutnika(int id, TipPutnika tipPutnika)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tipPutnika.Id)
            {
                return BadRequest();
            }

            db.Entry(tipPutnika).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipPutnikaExists(id))
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

        // POST: api/TipPutnikas
        [ResponseType(typeof(TipPutnika))]
        public IHttpActionResult PostTipPutnika(TipPutnika tipPutnika)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.TipPutnika.Add(tipPutnika);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tipPutnika.Id }, tipPutnika);
        }

        // DELETE: api/TipPutnikas/5
        [ResponseType(typeof(TipPutnika))]
        public IHttpActionResult DeleteTipPutnika(int id)
        {
            TipPutnika tipPutnika = db.TipPutnika.Find(id);
            if (tipPutnika == null)
            {
                return NotFound();
            }

            db.TipPutnika.Remove(tipPutnika);
            db.SaveChanges();

            return Ok(tipPutnika);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TipPutnikaExists(int id)
        {
            return db.TipPutnika.Count(e => e.Id == id) > 0;
        }
    }
}