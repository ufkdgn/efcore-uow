using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Horizon.DataOperation.Framework
{
    public class DbModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public bool IsRecordValid { get; set; }
        [ConcurrencyCheck]
        public Guid Version { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? TimeStamp { get; set; }
    }
}
