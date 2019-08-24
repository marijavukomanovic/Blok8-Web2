namespace WebApp.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using WebApp.Models;
    using WebApp.Models.Entiteti;

    internal sealed class Configuration : DbMigrationsConfiguration<WebApp.Persistence.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(WebApp.Persistence.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Admin" };

                manager.Create(role);
            }

            if (!context.Roles.Any(r => r.Name == "Controller"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Controller" };

                manager.Create(role);
            }

            if (!context.Roles.Any(r => r.Name == "AppUser"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "AppUser" };

                manager.Create(role);
            }

            #region VrstaPutnika(Student,Penzioner,Regularan)
            if (!context.TipPutnika.Any(t => t.Naziv == "Student"))
            {
                Models.Entiteti.TipPutnika tp1 = new Models.Entiteti.TipPutnika();
                tp1.Id = 1;
                tp1.Naziv = "Student";
                tp1.Koeficijent = 0.8;
                context.TipPutnika.Add(tp1);
                context.SaveChanges();
            }
            if (!context.TipPutnika.Any(t => t.Naziv == "Penziner"))
            {
                Models.Entiteti.TipPutnika tp2 = new Models.Entiteti.TipPutnika();
                tp2.Id = 2;
                tp2.Naziv = "Penziner";
                tp2.Koeficijent = 0.65;
                context.TipPutnika.Add(tp2);
                context.SaveChanges();
            }
            if (!context.TipPutnika.Any(t => t.Naziv == "Regularan"))
            {
                Models.Entiteti.TipPutnika tp3 = new Models.Entiteti.TipPutnika();
                tp3.Id = 3;
                tp3.Naziv = "Regularan";
                tp3.Koeficijent = 1.0;
                context.TipPutnika.Add(tp3);
                context.SaveChanges();
            }
            #endregion


            #region VrstaKarte(Vremenska,Dnevna,Mesecna,Godisnja)
            if (!context.TipKarte.Any(t => t.Naziv == "Vremenska"))
            {
                Models.Entiteti.TipKarte tk1 = new Models.Entiteti.TipKarte();
                tk1.Id = 1;
                tk1.Naziv = "Vremenska";
                context.TipKarte.Add(tk1);
                context.SaveChanges();
            }
            if (!context.TipKarte.Any(t => t.Naziv == "Dnevna"))
            {
                Models.Entiteti.TipKarte tk2 = new Models.Entiteti.TipKarte();
                tk2.Id = 2;
                tk2.Naziv = "Dnevna";
                context.TipKarte.Add(tk2);
                context.SaveChanges();
            }
            if (!context.TipKarte.Any(t => t.Naziv == "Mesecna"))
            {
                Models.Entiteti.TipKarte tk3 = new Models.Entiteti.TipKarte();
                tk3.Id = 3;
                tk3.Naziv = "Mesecna";
                context.TipKarte.Add(tk3);
                context.SaveChanges();
            }
            if (!context.TipKarte.Any(t => t.Naziv == "Godisnja"))
            {
                Models.Entiteti.TipKarte tk4 = new Models.Entiteti.TipKarte();
                tk4.Id = 4;
                tk4.Naziv = "Godisnja";
                context.TipKarte.Add(tk4);
                context.SaveChanges();
            }
            #endregion


            #region TipDana(Radni,Subota,Nedelja)
            if (!context.TipDana.Any(t => t.Tip == "Radni"))
            {
                Models.Entiteti.TipDana td1 = new Models.Entiteti.TipDana();
                td1.Id = 1;
                td1.Tip = "Radni";
                context.TipDana.Add(td1);
                context.SaveChanges();
            }
            if (!context.TipDana.Any(t => t.Tip == "Subota"))
            {
                Models.Entiteti.TipDana td2 = new Models.Entiteti.TipDana();
                td2.Id = 2;
                td2.Tip = "Subota";
                context.TipDana.Add(td2);
                context.SaveChanges();
            }
            if (!context.TipDana.Any(t => t.Tip == "Nedelja"))
            {
                Models.Entiteti.TipDana td3 = new Models.Entiteti.TipDana();
                td3.Id = 3;
                td3.Tip = "Nedelja";
                context.TipDana.Add(td3);
                context.SaveChanges();
            }
            #endregion


            #region TipLinije(Gradska,Prigradska)
            if (!context.TipLinija.Any(t => t.Naziv == "Gradski"))
            {
                Models.Entiteti.TipLinije tl1 = new Models.Entiteti.TipLinije();
                tl1.Id = 1;
                tl1.Naziv = "Gradski";
                context.TipLinija.Add(tl1);
                context.SaveChanges();
            }

            if (!context.TipLinija.Any(t => t.Naziv == "Prigradski"))
            {
                Models.Entiteti.TipLinije tl2 = new Models.Entiteti.TipLinije();
                tl2.Id = 2;
                tl2.Naziv = "Prigradski";
                context.TipLinija.Add(tl2);
                context.SaveChanges();
            }
            #endregion


            if (!context.Stanice.Any(t => t.Naziv == "1A"))
            {
                //Models.Entiteti.Stanica tl2 = new Models.Entiteti.Stanica();
                Stanica s1 = new Stanica();
                s1.Naziv = "1A";
                s1.Adresa = "Selj buna 55";
                s1.GeografskeKoordinataX = 1.0;
                s1.GeografskeKoordinataY = 2.0;
                s1.Aktivna = true;
                context.Stanice.Add(s1);
                context.SaveChanges();

                Linija l1 = new Linija();
                l1.Aktivna = true;
                l1.RedBroj = 1;
                l1.TipId = 1;
               // l1.Stanice.Add(s1);
                context.Linije.Add(l1);
                context.SaveChanges();
            }
            if (!context.RedoviVoznje.Any(t => t.Id == 1))
            {
                RedVoznje redVoznje = new RedVoznje();
                redVoznje.Id = 1;
                redVoznje.LinijaId = 1;
                redVoznje.RasporedVoznje = "04:30-04:50\n05:20-05:40\n06:00-06:20-06:40\n07:00-07:20-07:40\n08:00-08:20-08:40\n09:00-09:20-09:40\n10:15-10:20\n11:00-11:30\n12:00";
                redVoznje.TipDanaId = 1;
                context.RedoviVoznje.Add(redVoznje);
                context.SaveChanges();

            }

            if (!context.Cenovnici.Any(t => t.Id == 1))
            {
                Cenovnik cenovnik = new Cenovnik();
                cenovnik.Id = 1;
                cenovnik.VazenjeDo = new DateTime(2019, 8, 26);
                cenovnik.VazenjeOd = new DateTime(2019, 8, 20);
                context.Cenovnici.Add(cenovnik);
                context.SaveChanges();
            }
            if (!context.CenaKarata.Any(t => t.Id == 1))
            {
                CenaKarte cenaKarte1 = new CenaKarte();
                cenaKarte1.Id = 1;
                cenaKarte1.Cena = 65;
                cenaKarte1.CenovnikId = 1;
                cenaKarte1.TipKarteId = 1;
                context.CenaKarata.Add(cenaKarte1);
                context.SaveChanges();

                CenaKarte cenaKarte2 = new CenaKarte();
                cenaKarte2.Id = 2;
                cenaKarte2.Cena = 250;
                cenaKarte2.CenovnikId = 1;
                cenaKarte2.TipKarteId = 2;
                context.CenaKarata.Add(cenaKarte2);
                context.SaveChanges();

                CenaKarte cenaKarte3 = new CenaKarte();
                cenaKarte3.Id = 3;
                cenaKarte3.Cena = 1560;
                cenaKarte3.CenovnikId = 1;
                cenaKarte3.TipKarteId = 3;
                context.CenaKarata.Add(cenaKarte3);
                context.SaveChanges();

                CenaKarte cenaKarte4 = new CenaKarte();
                cenaKarte4.Id = 4;
                cenaKarte4.Cena = 12560;
                cenaKarte4.CenovnikId = 1;
                cenaKarte4.TipKarteId = 4;
                context.CenaKarata.Add(cenaKarte4);

                context.SaveChanges();
            }



            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);

            if (!context.Users.Any(u => u.UserName == "admin@yahoo.com"))
            {
                var user = new ApplicationUser() { Id = "admin", UserName = "admin@yahoo.com", Email = "admin@yahoo.com", PasswordHash = ApplicationUser.HashPassword("Admin123!")/*,TipPutnikaId = 1*/,/*KorisnikId=1*/ };
                userManager.Create(user);
                userManager.AddToRole(user.Id, "Admin");
            }

            if (!context.Users.Any(u => u.UserName == "appu@yahoo.com"))
            { 
                var user = new ApplicationUser() { Id = "appu", UserName = "appu@yahoo.com", Email = "appu@yahoo.com", PasswordHash = ApplicationUser.HashPassword("Appu123!") /*, TipPutnikaId = 1*/,/*KorisnikId=2*/ };
                userManager.Create(user);
                userManager.AddToRole(user.Id, "AppUser");
            }
        }
    }
}
