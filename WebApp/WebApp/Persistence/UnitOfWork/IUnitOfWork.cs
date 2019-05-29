using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Persistence.Repository;

namespace WebApp.Persistence.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        ILinijaRepository linijaRepository { get; set; }
         IStanicaRepository stanicaRepository { get; set; }
         IAutobusRepository autobusRepository { get; set; }

        ITipKarteRepository tipKarteRepository { get; set; }

        IVrstaKarteRepository vrstaKarteRepository { get; set; }

        IRedVoznjeRepository redVoznjeRepository { get; set; }

        int Complete();
    }
}
