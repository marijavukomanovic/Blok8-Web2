using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using WebApp.Models;
using WebApp.Models.Entiteti;
using WebApp.Persistence;
using WebApp.Persistence.Repository;

namespace WebApp.Controllers
{
    [Authorize]
    [RoutePrefix("api/Korisnik")]
    public class KorisniksController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private IKorisnikRepository korisnikRepository;
        private ITipPutnikaRepository tipPutnikaRepository;

        public KorisniksController(IKorisnikRepository korisnikRepository, ITipPutnikaRepository tipPutnikaRepository)
        {
            this.korisnikRepository = korisnikRepository;
            this.tipPutnikaRepository = tipPutnikaRepository;

        }

        [AllowAnonymous]
        [Route("Registracija")]
        public async Task<IHttpActionResult> Registracija(UserRegistrationBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (korisnikRepository.Find(x => x.Email == model.Email).Count() != 0)
            {
                return BadRequest("Vec ste registrovani sa datim meilom");
            }
            if (korisnikRepository.Find(x => x.KorisnickoIme == model.UserName).Count() != 0)
            {
                return BadRequest("Korisnicko ime se vec koristi");
            }
            var userStore = new UserStore<ApplicationUser>(db);
            var userManager = new UserManager<ApplicationUser>(userStore);
            DateTime datumRodjenja = DateTime.Parse(model.BirthdayDate);
            /*int ageGroup = 1;
            switch (model.PassengerType.ToString())
            {
                
                case "Student":
                    ageGroup = 1;
                    break;
                case "Penzioner":
                    ageGroup = 2;
                    break;
                case "Regularan":
                    ageGroup = 3;
                    break;
                    
                default:
                    ageGroup = 3;
                    break;
            }*/

            //int br = 0;
            //Int32.TryParse(model.PassengerType,out br);
            Korisnik noviKorisnik = new Korisnik()
            {
                KorisnickoIme = model.UserName,
                Prezime = model.LastName,
                Sifra = model.Password,
                Email = model.Email,
                Adresa = model.Address,
                DatumRodjenja = datumRodjenja,
                Tip = tipPutnikaRepository.Get(Convert.ToInt32(model.PassengerType)),
            };
            
            db.Korisnik.Add(noviKorisnik);
            db.SaveChanges();
           // noviKorisnik.Id = noviKorisnik.Id + 2;

            var appUser = new ApplicationUser() { Id = noviKorisnik.TipId.ToString(), UserName = noviKorisnik.KorisnickoIme, Email = noviKorisnik.Email, PasswordHash = ApplicationUser.HashPassword(noviKorisnik.Sifra) };
            IdentityResult result = await userManager.CreateAsync(appUser, noviKorisnik.Sifra);
            userManager.AddToRole(appUser.Id,"AppUser" );
            noviKorisnik.Sifra = appUser.PasswordHash;
            db.Entry(noviKorisnik).State = EntityState.Modified;
            db.SaveChanges();

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }
            return Ok();

        }
        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }


        // GET: api/Korisniks
        //public IQueryable<Korisnik> GetKorisnik()
        //{
        //    return db.Korisnik;
        //}

        //// GET: api/Korisniks/5
        //[ResponseType(typeof(Korisnik))]
        //public IHttpActionResult GetKorisnik(int id)
        //{
        //    Korisnik korisnik = db.Korisnik.Find(id);
        //    if (korisnik == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(korisnik);
        //}

        //// PUT: api/Korisniks/5
        //[ResponseType(typeof(void))]
        //public IHttpActionResult PutKorisnik(int id, Korisnik korisnik)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != korisnik.Id)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(korisnik).State = EntityState.Modified;

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!KorisnikExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return StatusCode(HttpStatusCode.NoContent);
        //}

        //// POST: api/Korisniks
        //[ResponseType(typeof(Korisnik))]
        //public IHttpActionResult PostKorisnik(Korisnik korisnik)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.Korisnik.Add(korisnik);
        //    db.SaveChanges();

        //    return CreatedAtRoute("DefaultApi", new { id = korisnik.Id }, korisnik);
        //}

        //// DELETE: api/Korisniks/5
        //[ResponseType(typeof(Korisnik))]
        //public IHttpActionResult DeleteKorisnik(int id)
        //{
        //    Korisnik korisnik = db.Korisnik.Find(id);
        //    if (korisnik == null)
        //    {
        //        return NotFound();
        //    }

        //    db.Korisnik.Remove(korisnik);
        //    db.SaveChanges();

        //    return Ok(korisnik);
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        //private bool KorisnikExists(int id)
        //{
        //    return db.Korisnik.Count(e => e.Id == id) > 0;
        //}
    }
}