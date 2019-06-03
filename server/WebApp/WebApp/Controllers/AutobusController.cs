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
    public class AutobusController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Autobus
        public IQueryable<Autobus> GetAutobusi()
        {
            return db.Autobusi;
        }

        // GET: api/Autobus/5
        [ResponseType(typeof(Autobus))]
        public IHttpActionResult GetAutobus(int id)
        {
            Autobus autobus = db.Autobusi.Find(id);
            if (autobus == null)
            {
                return NotFound();
            }

            return Ok(autobus);
        }

        // PUT: api/Autobus/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAutobus(int id, Autobus autobus)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != autobus.Id)
            {
                return BadRequest();
            }

            db.Entry(autobus).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AutobusExists(id))
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

        // POST: api/Autobus
        [ResponseType(typeof(Autobus))]
        public IHttpActionResult PostAutobus(Autobus autobus)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Autobusi.Add(autobus);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = autobus.Id }, autobus);
        }

        // DELETE: api/Autobus/5
        [ResponseType(typeof(Autobus))]
        public IHttpActionResult DeleteAutobus(int id)
        {
            Autobus autobus = db.Autobusi.Find(id);
            if (autobus == null)
            {
                return NotFound();
            }

            db.Autobusi.Remove(autobus);
            db.SaveChanges();

            return Ok(autobus);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AutobusExists(int id)
        {
            return db.Autobusi.Count(e => e.Id == id) > 0;
        }
    }
}