using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
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
        private IKartaRepository kartaRepository;
        private ICenaKarteRepository cenaKarteRepository;
        private ICenovnikRepository cenovnikRepository;


        private static readonly Object lockObj = new Object();

        public KorisniksController(IKorisnikRepository korisnikRepository, ITipPutnikaRepository tipPutnikaRepository, IKartaRepository kartaRepository, ICenaKarteRepository cenaKarteRepository, ICenovnikRepository cenovnikRepository)
        {
            this.cenovnikRepository = cenovnikRepository;
            this.cenaKarteRepository = cenaKarteRepository;
            this.kartaRepository = kartaRepository;
            this.korisnikRepository = korisnikRepository;
            this.tipPutnikaRepository = tipPutnikaRepository;

        }

        [Authorize(Roles = "AppUser")]
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
            var userManager = new UserManager<ApplicationUser>(userStore); // u njega se rola dodaje
            DateTime datumRodjenja = DateTime.Parse(model.BirthdayDate);
            int idKorisnika = korisnikRepository.GetAll().Count();
            Korisnik noviKorisnik = new Korisnik()
            {
                Id = ++idKorisnika,
                KorisnickoIme = model.UserName,
                Ime = model.Name,
                Prezime = model.LastName,
                Sifra = model.Password,
                Email = model.Email,
                Adresa = model.Address,
                DatumRodjenja = datumRodjenja,

                TipId = Convert.ToInt32(model.PassengerType),
                Document = model.Document,
                StatusId = 1,
            };

            db.Korisnik.Add(noviKorisnik);
            db.SaveChanges();
            // noviKorisnik.Id = noviKorisnik.Id + 2;

            var appUser = new ApplicationUser() { Id = noviKorisnik.KorisnickoIme, UserName = noviKorisnik.KorisnickoIme, Email = noviKorisnik.Email, PasswordHash = ApplicationUser.HashPassword(noviKorisnik.Sifra), KorisnikId = noviKorisnik.Id };
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

            //IList<string > ls = userManager.GetRoles(appUser.Id);

            return Ok();


        }
        // GET: api/Korisnik/GetInfo
        [Authorize(Roles = "AppUser")] //nek bude zakomentarisano da nam ne bi pravilo problem
        //[AllowAnonymous]
        [System.Web.Http.HttpGet]
        [Route("GetInfo/{username}")]
        [ResponseType(typeof(UserVerificationBindingModel))]
        public IHttpActionResult GetInfo(string username)//saljes mi localStorage.username
        {
            Korisnik user = (Korisnik)korisnikRepository.GetAll().Where(x => x.KorisnickoIme == username).ToList().First();
            if (user == null)
            {
                return NotFound();
            }
            UserVerificationBindingModel userRetval = new UserVerificationBindingModel()
            {
                Email = user.Email,
                Name = user.Ime,
                LastName = user.Prezime,
                UserName = user.KorisnickoIme,
                BirthdayDate = user.DatumRodjenja.ToString(),
                PassengerType = user.TipId,
                Document = user.Document,
                Address = user.Adresa,
            };
            switch (user.StatusId)
            {
                case 1:
                    userRetval.StatusVerifikacije = "Obrada";
                    break;
                case 2:
                    userRetval.StatusVerifikacije = "Verifikovan";
                    break;
                case 3:
                    userRetval.StatusVerifikacije = "Odbijen";
                    break;
            }




            return Ok(userRetval);
        }
        // POST api/Korisnik/ChangeInfo
        [Authorize(Roles = "AppUser")]
        //[AllowAnonymous]
        [System.Web.Http.HttpPost]
        [Route("ChangeInfo/{model}")]
        // [ResponseType(typeof(UserRegistrationBindingModel))]
        public IHttpActionResult ChangeInfo(UserVerificationBindingModel model)//saljes mi ceo model,mozes diseblovati  polja za meil i korisnicko imeposto ona ne smeju da se menjaju
        {
            lock (lockObj)

            {  // validacija
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

                // parsiranje datuma
                DateTime birthday = DateTime.Parse(model.BirthdayDate);

                // ako se promenila vrednost PassengerType-a
                if (user.TipId != model.PassengerType)
                {
                    //i ta nova vrednost nije Regular => treba vratiti status na pending, i obrisati sliku(ako nije promenjena)
                    if (model.PassengerType != 1)
                    {
                        user.StatusId = 1;
                    }
                    // i ta nova vrednost je Regular => treba postaviti status na succecssfull, i obrisati sliku (ako nije promenjena)
                    else
                    {
                        user.StatusId = 2;
                    }

                    // brisanje slike, ako nije promenjena (i ako je uopste pre toga imao sliku)
                    // ako je promenio grupu, a nije promenio sliku, treba obrisati njegovu sliku (obrisati i ne postaviti opet istu sliku)
                    if (user.Document != null)
                    {
                        if (user.Document.SequenceEqual(model.Document))
                        {
                            user.Document = null;
                            // user.StatusId = 3;
                        }
                        else
                        {
                            // promenio je starosnu grupu, i postavio novi dokument => novi dokument se smesta u bazu i ceka se kontroler da potvrdi/odbije
                            user.Document = model.Document;
                            user.StatusId = 1;

                        }
                    }
                    else
                    {
                        user.Document = model.Document;
                        user.StatusId = 1;
                    }
                }
                // ako nije promenio grupu, a promenio je sliku, treba sacuvati novu sliku i promeniti status na Pending
                else
                {
                    if (user.Document != null)
                    {
                        if (!user.Document.SequenceEqual(model.Document))
                        {
                            user.Document = model.Document;
                            user.StatusId = 1;
                        }
                    }
                    else
                    {
                        user.Document = model.Document;
                        user.StatusId = 1;
                    }
                }


                // izmena zeljenih propertija
                user.Ime = model.Name;
                user.Prezime = model.LastName;
                user.Adresa = model.Address;
                user.TipId = model.PassengerType;
                user.DatumRodjenja = birthday;
                user.Document = model.Document;


                // izmena u bazi
                //    korisnikRepository.Update(user);                      // ne radi kad koristim Repository metodu...
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();

                return Ok();
            }
        }
        [Authorize(Roles = "Controller")]
        //[AllowAnonymous]
        [System.Web.Http.HttpPost]
        [Route("IzlistajKlijente/{username}")]
        [ResponseType(typeof(UserVerificationBindingModel))]
        public IHttpActionResult IzlistajKlijente(string username)//saljes mi user name ja ti saljem listu sa svim korinicima koja i maju sve podatke u sebi
        {
            List<UserVerificationBindingModel> retVal = new List<UserVerificationBindingModel>();
            foreach (var korisnik in korisnikRepository.GetAll())
            {
                if (!(korisnik.KorisnickoIme.Equals(username) || korisnik.KorisnickoIme.Equals("admin")))
                {
                    UserVerificationBindingModel dodajKorisni = new UserVerificationBindingModel()
                    {
                        Name = korisnik.Ime,
                        LastName = korisnik.Prezime,
                        UserName = korisnik.KorisnickoIme,
                        Email = korisnik.Email,
                        Address = korisnik.Adresa,
                        BirthdayDate = korisnik.DatumRodjenja.ToLongDateString(),
                        Document = korisnik.Document,
                        PassengerType = korisnik.TipId,

                    };
                    switch (korisnik.StatusId)
                    {
                        case 1:
                            dodajKorisni.StatusVerifikacije = "Obrada";
                            break;
                        case 2:
                            dodajKorisni.StatusVerifikacije = "Verifikovan";
                            break;
                        case 3:
                            dodajKorisni.StatusVerifikacije = "Odbijen";
                            break;
                    }
                    retVal.Add(dodajKorisni);
                }
            }
            return Ok(retVal);
        }

        [Authorize(Roles = "Controller")]
        //[AllowAnonymous]
        [System.Web.Http.HttpGet]
        [Route("VerifikacijaKlijenta/{username}/{status}")]
        public IHttpActionResult VerifikacijaKlijenta(string username, string status)//saljes mi user name ja ti saljem listu sa svim korinicima koja i maju sve podatke u sebi
        {
            foreach (var korisnik in korisnikRepository.GetAll())
            {
                if (korisnik.KorisnickoIme.Equals(username))
                {
                    switch (status)
                    {
                        case "Obrada":
                            korisnik.StatusId = 1;
                            break;
                        case "Verifikovan":
                            korisnik.StatusId = 2;
                            break;
                        case "Odbijen":
                            korisnik.StatusId = 3;
                            break;
                    }
                    db.Entry(korisnik).State = EntityState.Modified;
                    db.SaveChanges();
                    break;
                }

            }

            return Ok();
        }


        [Authorize(Roles = "Controller")]
        //[AllowAnonymous]
        [System.Web.Http.HttpGet]
        [Route("IzlistajKarte")]
        [ResponseType(typeof(List<string>))]
        public IHttpActionResult IzlistajKarte()
        {
            List<string> retVal = new List<string>();
            string idKarte = "";
            foreach (var katre in kartaRepository.GetAll())
            {
                idKarte = katre.Id.ToString();
                retVal.Add(idKarte);
                idKarte = "";

            }



            return Ok(retVal);
        }
        [Authorize(Roles = "Controller")]

        [System.Web.Http.HttpGet]
        [Route("VerifikujKartu/{idKarta}")]//saljem ti string da li je verifikovaan ili ne

        [ResponseType(typeof(string))]
        public IHttpActionResult VerifikujKartu(string idKarta)
        {
            DateTime VremeKupovine = new DateTime();
            DateTime VremeVavenja = new DateTime();
            DateTime sad = DateTime.Now;
            int idCenaKarte = -1;
            int idcenovnik = -1;
            string retVal = "";
            VremeKupovine = kartaRepository.Get(Convert.ToInt32(idKarta)).VremeKupovine;
            idCenaKarte = kartaRepository.Get(Convert.ToInt32(idKarta)).CenaKarteId;
            idcenovnik = cenaKarteRepository.Get(idCenaKarte).CenovnikId;
            VremeVavenja = cenovnikRepository.Get(idcenovnik).VazenjeDo;
            if (DateTime.Compare(sad, VremeVavenja) < 0)
            {
                kartaRepository.Get(Convert.ToInt32(idKarta)).Verifikovana = false;
                retVal = "Odbijena";
                foreach (var k in kartaRepository.GetAll())
                {
                    if (k.Id == Convert.ToInt32(idKarta))
                    {
                        db.Entry(k).State = EntityState.Modified;
                        db.SaveChanges();
                        break;
                    }
                }
            }
            else
            {
                kartaRepository.Get(Convert.ToInt32(idKarta)).Verifikovana = true;
                retVal = "Verifikovana";
                foreach (var k in kartaRepository.GetAll())
                {
                    if (k.Id == Convert.ToInt32(idKarta))
                    {
                        db.Entry(k).State = EntityState.Modified;
                        db.SaveChanges();
                        break;
                    }
                }
            }

           

            return Ok(retVal);
        }


        /*     [AllowAnonymous]
         [HttpPost]
         [Route("UplaodPicture/{username}")]

         public IHttpActionResult UploadImage(string username)
         {
             var httpRequest = HttpContext.Current.Request;

             try
             {
                 if (httpRequest.Files.Count > 0)
                 {
                     foreach (string file in httpRequest.Files)
                     {

                         //ApplicationUser ret = new ApplicationUser();
                         Korisnik ret = new Korisnik();

                         var userStore = new UserStore<ApplicationUser>(db);
                         var userManager = new UserManager<ApplicationUser>(userStore);

                         List<ApplicationUser> list = userManager.Users.ToList();


                         foreach (Korisnik a in korisnikRepository.GetAll())
                         {
                             if (a.KorisnickoIme.Equals(username))
                             {
                                 ret = a;
                                 break;
                             }
                         }

                         if (ret == null)
                         {
                             return BadRequest("User does not exists.");
                         }

                         if (ret.Document != null)
                         {
                             File.Delete(HttpContext.Current.Server.MapPath("~/UploadFile/" + ret.Document));
                         }

                         var postedFile = httpRequest.Files[file];
                         string fileName = username + "_" + postedFile.FileName;
                         var filePath = HttpContext.Current.Server.MapPath("~/UploadFile/" + fileName);

                         ret.Document = fileName;

                         db.Entry(ret).State = EntityState.Modified;

                         try
                         {
                             db.SaveChanges();
                         }
                         catch (DbUpdateConcurrencyException e)
                         {
                             return StatusCode(HttpStatusCode.BadRequest);
                         }

                         postedFile.SaveAs(filePath);
                     }

                     return Ok();
                 }
                 else
                 {
                     return BadRequest();
                 }
             }
             catch (Exception e)
             {
                 return InternalServerError(e);
             }

         }*/


        #region Njihovi kontroleri
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

        #endregion
    }
}