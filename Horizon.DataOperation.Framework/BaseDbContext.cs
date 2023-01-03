using Microsoft.EntityFrameworkCore;

namespace Horizon.DataOperation.Framework
{
    public class BaseDbContext : DbContext
    {
        public DatabaseContextSettings Settings { get; private set; }


        private BaseDbContext()
        {
            this.Settings = null;
        }

        public BaseDbContext(DatabaseContextSettings settings)
        {
            this.Settings = settings;
            Database.SetCommandTimeout(settings.Timeout);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            foreach (var item in ChangeTracker.Entries().Where(x => x.State == EntityState.Added))
            {
                var dbModel = item.Entity as DbModel;
                if (dbModel == null) continue;

                dbModel.TimeStamp = DateTime.UtcNow;
                dbModel.IsRecordValid = true;
                // generate Row Version
                dbModel.Version = Guid.NewGuid();
            }
            return base.SaveChanges();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (Settings == null) throw new FileNotFoundException($"Database settings are not provided.");

            if (Settings.DbProviderType == DbProviderType.MSSql)
            {
                optionsBuilder.UseSqlServer(Settings.ConnectionString);
            }
            else
            {
                throw new NotSupportedException($"Database Provider Type {Settings.DbProviderType.ToString()} is not supported yet.");
            }
            base.OnConfiguring(optionsBuilder);
        }
    }
}
