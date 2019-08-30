﻿using Microsoft.AspNet.Identity;
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
            if (korisnikRepository.Find(x => x.KorisnickoIme == model.UserName).Count() != 0)//nama je mil i korisnicko ime isto
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
                Ime = model.Name,
                Prezime = model.LastName,
                Sifra = model.Password,
                Email = model.Email,
                Adresa = model.Address,
                DatumRodjenja = datumRodjenja,

                TipId = Convert.ToInt32(model.PassengerType),
                Document = model.Document,
            };

            db.Korisnik.Add(noviKorisnik);
            db.SaveChanges();
            // noviKorisnik.Id = noviKorisnik.Id + 2;

            var appUser = new ApplicationUser() { Id =noviKorisnik.KorisnickoIme, UserName = noviKorisnik.KorisnickoIme, Email = noviKorisnik.Email, PasswordHash = ApplicationUser.HashPassword(noviKorisnik.Sifra), KorisnikId = noviKorisnik.Id };
            appUser.Id = noviKorisnik.Id.ToString();
            IdentityResult result = await userManager.CreateAsync(appUser, noviKorisnik.Sifra);
            userManager.AddToRole(appUser.Id, "AppUser");
            noviKorisnik.Sifra = appUser.PasswordHash;
            db.Entry(noviKorisnik).State = EntityState.Modified;
            db.SaveChanges();

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }
            return Ok();

        }
        // GET: api/Korisnik/GetInfo
        // [Authorize(Roles = "AppUser")] //nek bude zakomentarisano da nam ne bi pravilo problem
        [AllowAnonymous]
        [System.Web.Http.HttpGet]
        [Route("GetInfo/{username}")]
        [ResponseType(typeof(UserRegistrationBindingModel))]
        public IHttpActionResult GetInfo(string username)//saljes mi localStorage.username
        {
            Korisnik user = (Korisnik)korisnikRepository.GetAll().Where(x => x.KorisnickoIme == username).ToList().First();
            if (user == null)
            {
                return NotFound();
            }

            //string ageGroup="";
            //switch (user.TipId)
            //{
            //    case 1:
            //        ageGroup = "Student";
            //        break;
            //    case 2:
            //        ageGroup = "Penzioner";
            //        break;
            //    case 3:
            //        ageGroup = "Regularan";
            //        break;
            //    default:
            //        ageGroup = "None";
            //        break;
            //}

            UserRegistrationBindingModel userRetval = new UserRegistrationBindingModel()
            {
                Email = user.Email,
                Password = user.Sifra,
                ConfirmPassword = user.Sifra,
                Name = user.Ime,
                LastName = user.Prezime,
                UserName = user.KorisnickoIme,
                BirthdayDate = user.DatumRodjenja.ToString(),
                PassengerType = user.TipId,//ovo cemo verovatno morati da prepravimo
                Document = user.Document,
                Address = user.Adresa
            };

            return Ok(userRetval);
        }
        // POST api/Korisnik/ChangeInfo
        //  [Authorize(Roles = "AppUser")]
        [AllowAnonymous]
        [System.Web.Http.HttpPost]
        [Route("ChangeInfo/{model}")]
       // [ResponseType(typeof(UserRegistrationBindingModel))]
        public IHttpActionResult ChangeInfo(UserChangeInfoBindingModel model)//saljes mi ceo model,mozes diseblovati  polja za meil i korisnicko imeposto ona ne smeju da se menjaju
        {
            // validacija
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // username i email se sigurno ne menjaju
            // nalazenje podataka o User-u preko username-a
            Korisnik user = (Korisnik)korisnikRepository.GetAll().Where(x => x.KorisnickoIme == model.UserName).ToList().First();
            if (user == null)
            {
                return NotFound();
            }

            // parsiranje ageGroup-a
            //int ageGroup = 1;
            //switch (model.PassengerType)
            //{
            //    case "Regular":
            //        ageGroup = 1;
            //        break;
            //    case "Student":
            //        ageGroup = 2;
            //        break;
            //    case "Pensioner":
            //        ageGroup = 3;
            //        break;
            //    default:
            //        ageGroup = 1;
            //        break;
            //}

            // parsiranje datuma
            DateTime birthday = DateTime.Parse(model.BirthdayDate);
            //posto nam slika jos ne radi ovo nam jos  ne treba !!!!!!!!!!!!!!!!
            // ako se promenila vrednost ageGroup-e
            //if (user.TipId != model.PassengerType)
            //{
            //    // i ta nova vrednost nije Regular => treba vratiti status na pending, i obrisati sliku (ako nije promenjena)
            //    if (model.PassengerType != 1)
            //    {
            //       // user.VerificationStatus = VerificationStatus.Pending;
            //    }
            //    // i ta nova vrednost je Regular => treba postaviti status na succecssfull, i obrisati sliku (ako nije promenjena)
            //    else
            //    {
            //        //user.VerificationStatus = VerificationStatus.Successful;
            //    }

            //    // brisanje slike, ako nije promenjena (i ako je uopste pre toga imao sliku)
            //    // ako je promenio grupu, a nije promenio sliku, treba obrisati njegovu sliku (obrisati i ne postaviti opet istu sliku)
            //    if (user.Document != null)
            //    {
            //        if (user.Document.SequenceEqual(model.Document))
            //        {
            //            user.Document = null;
            //        }
            //        else
            //        {
            //            // promenio je starosnu grupu, i postavio novi dokument => novi dokument se smesta u bazu i ceka se kontroler da potvrdi/odbije
            //            user.Document = model.Document;
            //        }
            //    }
            //    else
            //    {
            //        user.Document = model.Document;
            //       // user.VerificationStatus = VerificationStatus.Pending;
            //    }
            //}
            //// ako nije promenio grupu, a promenio je sliku, treba sacuvati novu sliku i promeniti status na Pending
            //else
            //{
            //    if (user.Document != null)
            //    {
            //        if (!user.Document.SequenceEqual(model.Document))
            //        {
            //            user.Document = model.Document;
            //           // user.VerificationStatus = VerificationStatus.Pending;
            //        }
            //    }
            //    else
            //    {
            //        user.Document = model.Document;
            //       // user.VerificationStatus = VerificationStatus.Pending;
            //    }
            //}kad slika bude radila ovo nam treba!!!!!!!!!!!!!!!!!!!!

            // izmena zeljenih propertija
            user.Ime = model.Name;
            user.Prezime = model.LastName;
            user.Adresa = model.Address;
            user.TipId = model.PassengerType;
            user.DatumRodjenja = birthday;
            if (!model.Password.Equals(model.ConfirmPassword))//za slucaj da se sifre ne poklapaju
            { return BadRequest("Sifre se moraju poklapati"); }
            user.Sifra = model.Password;


            // izmena u bazi
            //    korisnikRepository.Update(user);                      // ne radi kad koristim Repository metodu...
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();
            //string ageGroupString;
            //switch (user.TipId)
            //{
            //    case 1:
            //        ageGroupString = "Regular";
            //        break;
            //    case 2:
            //        ageGroupString = "Student";
            //        break;
            //    case 3:
            //        ageGroupString = "Pensioner";
            //        break;
            //    default:
            //        ageGroupString = "None";
            //        break;
            //}
            //UserRegistrationBindingModel userRetVal = new UserRegistrationBindingModel()
            //{
            //    Email = user.Email,
            //    Password = user.Sifra,
            //    ConfirmPassword = user.Sifra,
            //    Name = user.Ime,
            //    LastName = user.Prezime,
            //    UserName = user.KorisnickoIme,
            //    BirthdayDate = user.DatumRodjenja.ToString(),
            //    PassengerType = user.TipId,///vereovatnoi treba promeniti
            //    Document = user.Document,
            //    Address = user.Adresa
            //};
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
        public IQueryable<Korisnik> GetKorisnik()
        {
            return db.Korisnik;
        }

        // GET: api/Korisniks/5
        [ResponseType(typeof(Korisnik))]
        public IHttpActionResult GetKorisnik(int id)
        {
            Korisnik korisnik = db.Korisnik.Find(id);
            if (korisnik == null)
            {
                return NotFound();
            }

            return Ok(korisnik);
        }

        // PUT: api/Korisniks/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutKorisnik(int id, Korisnik korisnik)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != korisnik.Id)
            {
                return BadRequest();
            }

            db.Entry(korisnik).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KorisnikExists(id))
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

        // POST: api/Korisniks
        [ResponseType(typeof(Korisnik))]
        public IHttpActionResult PostKorisnik(Korisnik korisnik)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Korisnik.Add(korisnik);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = korisnik.Id }, korisnik);
        }

        // DELETE: api/Korisniks/5
        [ResponseType(typeof(Korisnik))]
        public IHttpActionResult DeleteKorisnik(int id)
        {
            Korisnik korisnik = db.Korisnik.Find(id);
            if (korisnik == null)
            {
                return NotFound();
            }

            db.Korisnik.Remove(korisnik);
            db.SaveChanges();

            return Ok(korisnik);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool KorisnikExists(int id)
        {
            return db.Korisnik.Count(e => e.Id == id) > 0;
        }
    }
}