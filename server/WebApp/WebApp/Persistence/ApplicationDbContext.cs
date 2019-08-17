using System;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using WebApp.Models;
using WebApp.Models.Entiteti;

namespace WebApp.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
       public DbSet<Linija> Linije { get; set; }

        public DbSet<Stanica> Stanice { get; set; }

        public DbSet<Autobus> Autobusi { get; set; }

        public DbSet<Karta> Karte { get; set; }

        public DbSet<Cenovnik> Cenovnici { get; set; }

        public DbSet<CenaKarte> CenaKarata { get; set; }

        // public DbSet<VremenskaKarta> VremenskeKarte { get; set; }
        public DbSet<TipKarte> TipKarte { get; set; }
        public DbSet<TipDana> TipDana  { get; set; }

        public DbSet<TipPutnika> TipPutnika { get; set; }

        public DbSet<RedVoznje> RedoviVoznje { get; set; }
        public DbSet<Korisnik> Korisnik { get; set; }
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<WebApp.Models.ApplicationUser> ApplicationUsers { get; set; }
    }
}