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


            if (!context.Stanice.Any(t => t.Naziv == "12A-1"))
            {
                Stanica s1 = new Stanica();
                s1.Id = 1;
                s1.Naziv = "12A-1";
                s1.Adresa = "Selj buna 55";
                s1.GeografskeKoordinataX = 45.242664693102796;
                s1.GeografskeKoordinataY = 19.841715892302318;
                s1.Aktivna = true;
                context.Stanice.Add(s1);
                context.SaveChanges();

                Stanica s2 = new Stanica();
                s2.Id = 2;
                s2.Naziv = "12A-2";
                s2.Adresa = "Selj buna 90";
                s2.GeografskeKoordinataX = 45.242154780020556 ;
                s2.GeografskeKoordinataY = 19.842096765982433 ;
                s2.Aktivna = true;
                context.Stanice.Add(s2);
                context.SaveChanges();

                Stanica s3 = new Stanica();
                s3.Id = 3;
                s3.Naziv = "12A-3";
                s3.Adresa = "Selj buna 189";
                s3.GeografskeKoordinataX =45.241693;
                s3.GeografskeKoordinataY =19.842440;
                s3.Aktivna = true;
                context.Stanice.Add(s3);
                context.SaveChanges();

                Stanica s4 = new Stanica();
                s4.Id = 4;
                s4.Naziv = "7A-1";
                s4.Adresa = "Selj buna 189";
                s4.GeografskeKoordinataX = 45.24200369378396;
                s4.GeografskeKoordinataY = 19.841640790368047;
                s4.Aktivna = true;
                context.Stanice.Add(s4);
                context.SaveChanges();

                Stanica s5 = new Stanica();
                s5.Id = 5;
                s5.Naziv = "7A-3";
                s5.Adresa = "Selj buna 189";
                s5.GeografskeKoordinataX = 45.24266091598567;
                s5.GeografskeKoordinataY = 19.843749006653752;
                s5.Aktivna = true;
                context.Stanice.Add(s5);
                context.SaveChanges();


                Linija l1 = new Linija();
                l1.Id = 1;
                l1.Opis = "Linija u adresi selj.buna";
                l1.Aktivna = true;
                l1.RedBroj = "12A";
                l1.TipId = 1;
                l1.Boja = "blue";
                context.Linije.Add(l1);
                context.SaveChanges();

                Linija l2 = new Linija();
                l2.Id = 2;
                l2.Opis = "Linija u adresi Bulevar osloboljenja";
                l2.Aktivna = true;
                l2.RedBroj = "7A";
                l2.TipId = 2;
                l2.Boja = "red";
                context.Linije.Add(l2);
                context.SaveChanges();

                Linija l3 = new Linija();
                l3.Id = 3;
                l3.Opis = "Linija u adresi maksima gorkog";
                l3.Aktivna = true;
                l3.RedBroj = "13A";
                l3.TipId = 2;
                l3.Boja = "orange";
                context.Linije.Add(l3);
                context.SaveChanges();

                Linija l4 = new Linija();
                l4.Id = 4;
                l4.Opis = "Linija u adresi dunavska";
                l4.Aktivna = true;
                l4.RedBroj = "18";
                l4.TipId = 2;
                l4.Boja = "green";
                context.Linije.Add(l4);
                context.SaveChanges();



                LinijeStanice linijeStanice1=new LinijeStanice();
                linijeStanice1.LinijeId = l1.Id;
                linijeStanice1.StaniceId = s1.Id;
                context.LinijeStanices.Add(linijeStanice1);
                context.SaveChanges();

                LinijeStanice linijeStanice2 = new LinijeStanice();
                linijeStanice2.LinijeId = l1.Id;
                linijeStanice2.StaniceId = s2.Id;
                context.LinijeStanices.Add(linijeStanice2);
                context.SaveChanges();

                LinijeStanice linijeStanice3 = new LinijeStanice();
                linijeStanice3.LinijeId = l1.Id;
                linijeStanice3.StaniceId = s3.Id;
                context.LinijeStanices.Add(linijeStanice3);
                context.SaveChanges();

                LinijeStanice linijeStanice4 = new LinijeStanice();
                linijeStanice4.LinijeId = l2.Id;
                linijeStanice4.StaniceId = s4.Id;
                context.LinijeStanices.Add(linijeStanice4);
                context.SaveChanges();

                LinijeStanice linijeStanice5 = new LinijeStanice();
                linijeStanice5.LinijeId = l2.Id;
                linijeStanice5.StaniceId = s2.Id;
                context.LinijeStanices.Add(linijeStanice5);
                context.SaveChanges();

                LinijeStanice linijeStanice6 = new LinijeStanice();
                linijeStanice6.LinijeId = l2.Id;
                linijeStanice6.StaniceId = s5.Id;
                context.LinijeStanices.Add(linijeStanice6);
                context.SaveChanges();

                LinijeStanice linijeStanice7 = new LinijeStanice();
                linijeStanice7.LinijeId = l3.Id;
                linijeStanice7.StaniceId = s1.Id;
                context.LinijeStanices.Add(linijeStanice7);
                context.SaveChanges();

                LinijeStanice linijeStanice8 = new LinijeStanice();
                linijeStanice8.LinijeId = l3.Id;
                linijeStanice8.StaniceId = s2.Id;
                context.LinijeStanices.Add(linijeStanice8);
                context.SaveChanges();

                LinijeStanice linijeStanice9 = new LinijeStanice();
                linijeStanice9.LinijeId = l1.Id;
                linijeStanice9.StaniceId = s5.Id;
                context.LinijeStanices.Add(linijeStanice9);
                context.SaveChanges();

                LinijeStanice linijeStanice10 = new LinijeStanice();
                linijeStanice10.LinijeId = l4.Id;
                linijeStanice10.StaniceId = s3.Id;
                context.LinijeStanices.Add(linijeStanice10);
                context.SaveChanges();

                LinijeStanice linijeStanice11 = new LinijeStanice();
                linijeStanice11.LinijeId = l4.Id;
                linijeStanice11.StaniceId = s2.Id;
                context.LinijeStanices.Add(linijeStanice11);
                context.SaveChanges();

                LinijeStanice linijeStanice12 = new LinijeStanice();
                linijeStanice12.LinijeId = l4.Id;
                linijeStanice12.StaniceId = s5.Id;
                context.LinijeStanices.Add(linijeStanice12);
                context.SaveChanges();
            }
            if (!context.RedoviVoznje.Any(t => t.Id == 1))
            {
                RedVoznje redVoznje = new RedVoznje();
                redVoznje.Id = 1;
                redVoznje.Aktivan = true;
                redVoznje.LinijaId = 1;
                redVoznje.RasporedVoznje = "04:30-04:50\n05:20-05:40\n06:00-06:20-06:40\n07:00-07:20-07:40\n08:00-08:20-08:40\n09:00-09:20-09:40\n10:15-10:20\n11:00-11:30\n12:00";
                redVoznje.TipDanaId = 1;
                context.RedoviVoznje.Add(redVoznje);
                context.SaveChanges();

                RedVoznje redVoznje1 = new RedVoznje();
                redVoznje1.Id = 2;
                redVoznje1.Aktivan = true;
                redVoznje1.LinijaId = 2;
                redVoznje1.RasporedVoznje = "04:30-04:50\n05:20-05:40\n06:00-06:20-06:40\n07:00-07:20-07:40\n08:00-08:20-08:40\n09:00-09:20-09:40\n10:15-10:20\n11:00-11:30\n12:00";
                redVoznje1.TipDanaId = 2;
                context.RedoviVoznje.Add(redVoznje1);
                context.SaveChanges();
            }

            if (!context.Cenovnici.Any(t => t.Id == 1))
            {
                Cenovnik cenovnik = new Cenovnik();
                cenovnik.Id = 1;
                cenovnik.VazenjeDo = new DateTime(2020, 2, 10,12,0,0);
                cenovnik.VazenjeOd = new DateTime(2019, 1, 27,11,59,59);
                cenovnik.Aktivan = true;
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

            if (!context.Statuss.Any(u => u.Naziv == "Obrada"))
            {
                Status noviStatus = new Status();
                noviStatus.Id = 1;
                noviStatus.Naziv = "Obrada";
                context.Statuss.Add(noviStatus);
                context.SaveChanges();

                Status noviStatus1 = new Status();
                noviStatus1.Id = 2;
                noviStatus1.Naziv = "Verifikovan";
                context.Statuss.Add(noviStatus1);
                context.SaveChanges();

                Status noviStatus2 = new Status();
                noviStatus2.Id = 3;
                noviStatus2.Naziv = "Odbijen";
                context.Statuss.Add(noviStatus2);
                context.SaveChanges();
            }

            if (!context.Users.Any(u => u.UserName == "admin"))
            {
                /*  Korisnik AdminK = new Korisnik() {Id=1 }; */
               
                var korisnik = new Korisnik();
                korisnik.Id = 1;
                korisnik.Ime = "Admin";
                korisnik.Prezime = "Adminic";
                korisnik.KorisnickoIme = "admin";
                korisnik.Sifra = ApplicationUser.HashPassword("Admin123!");
                korisnik.Adresa = "glupost 67";
                korisnik.DatumRodjenja = new DateTime(1997,4,5);
                korisnik.Email = "admin@yahoo.com";
                korisnik.TipId = 1;
                korisnik.StatusId = 2;
                
                context.Korisnik.Add(korisnik);
                context.SaveChanges();

                 var user = new ApplicationUser() { Id = "1", UserName = "admin", Email = "admin@yahoo.com", PasswordHash = ApplicationUser.HashPassword("Admin123!")};
                user.KorisnikId = korisnik.Id;
                
                userManager.Create(user);
                userManager.AddToRole(user.Id, "Admin");
            }

            if (!context.Users.Any(u => u.UserName == "appu"))
            {
                var korisnik = new Korisnik();
                korisnik.Id = 2;
                korisnik.Ime = "Marko";
                korisnik.Prezime = "Matic";
                korisnik.KorisnickoIme = "appu";
                korisnik.Sifra = ApplicationUser.HashPassword("Appu123!");
                korisnik.Adresa = "gogoljeva 67";
                korisnik.DatumRodjenja = new DateTime(1997, 9, 5);
                korisnik.Email = "appu@yahoo.com";
                korisnik.TipId = 3;
                korisnik.StatusId = 1;
                context.Korisnik.Add(korisnik);
                context.SaveChanges();

                var user = new ApplicationUser() { Id = "2", UserName = "appu", Email = "appu@yahoo.com", PasswordHash = ApplicationUser.HashPassword("Appu123!")};
                user.KorisnikId = korisnik.Id;
                userManager.Create(user);
                userManager.AddToRole(user.Id, "AppUser");
            }

            if (!context.Users.Any(u => u.UserName == "controll"))
            {
                var korisnik = new Korisnik();
                korisnik.Id = 3;
                korisnik.Ime = "Marko";
                korisnik.Prezime = "Matic";
                korisnik.KorisnickoIme = "controll";
                korisnik.Sifra = ApplicationUser.HashPassword("Controll123!");
                korisnik.Adresa = "gogoljeva 67";
                korisnik.DatumRodjenja = new DateTime(1997, 9, 5);
                korisnik.Email = "controll@yahoo.com";
                korisnik.TipId = 3;
                korisnik.StatusId = 2;
                context.Korisnik.Add(korisnik);
                context.SaveChanges();

                var user = new ApplicationUser() { Id = "3", UserName = "controll", Email = "controll@yahoo.com", PasswordHash = ApplicationUser.HashPassword("Controll123!") };
                user.KorisnikId = korisnik.Id;
                userManager.Create(user);
                userManager.AddToRole(user.Id, "Controller");
            }
        }
    }
}
