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
    public class StanicasController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Stanicas
        public IQueryable<Stanica> GetStanice()
        {
            return db.Stanice;
        }

        // GET: api/Stanicas/5
        [ResponseType(typeof(Stanica))]
        public IHttpActionResult GetStanica(string id)
        {
            Stanica stanica = db.Stanice.Find(id);
            if (stanica == null)
            {
                return NotFound();
            }

            return Ok(stanica);
        }

        // PUT: api/Stanicas/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutStanica(string id, Stanica stanica)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != stanica.Naziv)
            {
                return BadRequest();
            }

            db.Entry(stanica).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StanicaExists(id))
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

        // POST: api/Stanicas
        [ResponseType(typeof(Stanica))]
        public IHttpActionResult PostStanica(Stanica stanica)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Stanice.Add(stanica);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (StanicaExists(stanica.Naziv))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = stanica.Naziv }, stanica);
        }

        // DELETE: api/Stanicas/5
        [ResponseType(typeof(Stanica))]
        public IHttpActionResult DeleteStanica(string id)
        {
            Stanica stanica = db.Stanice.Find(id);
            if (stanica == null)
            {
                return NotFound();
            }

            db.Stanice.Remove(stanica);
            db.SaveChanges();

            return Ok(stanica);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StanicaExists(string id)
        {
            return db.Stanice.Count(e => e.Naziv == id) > 0;
        }
    }
}