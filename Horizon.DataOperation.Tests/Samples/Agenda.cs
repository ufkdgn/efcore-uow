using Horizon.DataOperation.Framework;
using System.ComponentModel.DataAnnotations.Schema;

namespace Horizon.DataOperation.Tests.Samples
{
    [Table("Agenda", Schema = "dbo")]
    public class Agenda : DbModel
    {
        public long UserId { get; set; }
        public string Title { get; set; }
        public long? LocationId { get; set; }
        public DateTime Date { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string Notes { get; set; }
        public bool? Shared { get; set; }
    }
}
