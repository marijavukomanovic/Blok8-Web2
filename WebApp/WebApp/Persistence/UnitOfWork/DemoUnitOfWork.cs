using System;
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
        public ILinijaRepository LinijaRepository { get; set; }
        [Dependency]
        public IStanicaRepository StanicaRepository { get; set; }
        [Dependency]
        public IAutobusRepository AutobusRepository { get; set; }
        [Dependency]
        public ITipKarteRepository TipKarteRepository { get; set; }
        [Dependency]
        public IVrstaKarteRepository VrstaKarteRepository { get; set; }
        [Dependency]
        public IRedVoznjeRepository RedVoznjeRepository { get; set; }
        public ILinijaRepository linijaRepository { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IStanicaRepository stanicaRepository { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IAutobusRepository autobusRepository { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public ITipKarteRepository tipKarteRepository { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IVrstaKarteRepository vrstaKarteRepository { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IRedVoznjeRepository redVoznjeRepository { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

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