﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Unity;
using WebApp.Persistence.Repository;

namespace WebApp.Persistence.UnitOfWork
{
    public class DemoUnitOfWork : IUnitOfWork
    {
        [Dependency]
        public ILinijaRepository linijaRepository { get ; set; }
        [Dependency]
        public IStanicaRepository stanicaRepository { get; set; }
        [Dependency]
        public IAutobusRepository autobusRepository  { get; set; } 
        [Dependency]
        public ITipKarteRepository tipKarteRepository { get; set; }
        [Dependency]
        public ITipDanaRepository tipDanaRepository { get; set; }
        [Dependency]
        public IRedVoznjeRepository redVoznjeRepository { get; set; }
        [Dependency]
        public IKartaRepository kartaRepository { get; set; }
        [Dependency]
        public ICenovnikRepository cenovnikRepository { get; set; }
        [Dependency]
        public ICenaKarteRepository cenaKarteRepository { get; set; }
        [Dependency]
        public ITipPutnikaRepository tipPutnikaRepository { get; set; }
        [Dependency]
        public IKorisnikRepository korisnikRepository { get; set; }
        [Dependency]
        public ITipLinijeRepository tipLinijeRepository { get; set; }
        [Dependency]
        public ILinijeStaniceRepository linijeStaniceRepository { get; set; }
        [Dependency]
        public IStatusRepository statusRepository { get; set; }

        [Dependency]
        public IPayPalRepository payPalRepository { get; set; }


        private readonly DbContext _context;

        public DemoUnitOfWork(DbContext context)
        {
            _context = context;
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}