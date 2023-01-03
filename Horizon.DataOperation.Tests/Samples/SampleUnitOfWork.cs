using Horizon.DataOperation.Framework;
using Microsoft.AspNetCore.Http;

namespace Horizon.DataOperation.Tests.Samples
{
    public class SampleUnitOfWork : BaseUnitOfWork<SampleDbContext>
    {
        public SampleUnitOfWork() : base(new SampleDbContext(DefaultSettings()))
        {
        }

        public SampleUnitOfWork(HttpContext httpContext) : base(new SampleDbContext(DefaultSettings()), httpContext)
        {
        }

        #region Agenda
        private Repository<Agenda> agendaRepository;
        public Repository<Agenda> AgendaRepository
        {
            get
            {
                if (agendaRepository == null)
                    agendaRepository = new Repository<Agenda>(this.Context);
                return agendaRepository;
            }
        }
        #endregion

        public static DatabaseContextSettings DefaultSettings()
        {
            DatabaseContextSettings settings = new DatabaseContextSettings();
            settings.ConnectionString = "";
            settings.DbProviderType = DbProviderType.MSSql;
            return settings;
        }
    }
}
