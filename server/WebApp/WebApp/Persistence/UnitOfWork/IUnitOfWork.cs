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

        ITipDanaRepository tipDanaRepository { get; set; }

        IRedVoznjeRepository redVoznjeRepository { get; set; }

        IKartaRepository kartaRepository { get; set; }

        ICenovnikRepository cenovnikRepository { get; set; }

        ICenaKarteRepository cenaKarteRepository { get; set; }

        ITipPutnikaRepository tipPutnikaRepository { get; set; }

        IKorisnikRepository korisnikRepository { get; set; }

        ITipLinijeRepository tipLinijeRepository { get; set; }
        ILinijeStaniceRepository linijeStaniceRepository { get; set; }
        IStatusRepository statusRepository { get; set; }

        int Complete();
    }
}
