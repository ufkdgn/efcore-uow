using Horizon.DataOperation.Framework;
using Microsoft.EntityFrameworkCore;

namespace Horizon.DataOperation.Tests.Samples
{
    public class SampleDbContext : BaseDbContext
    {
        public SampleDbContext(DatabaseContextSettings settings) : base(settings)
        {
        }

        public virtual DbSet<Agenda> Agenda { get; set; }
    }
}
