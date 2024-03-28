#region Using
using System.ComponentModel.DataAnnotations.Schema;
#endregion

namespace AuditNow.Core.Models
{
    public abstract class BaseEntity
    {

        public int CreationUserId { get; set; }

        public DateTime CreationDate { get; set; }

        public int ModificationUserId { get; set; }

        public DateTime ModificationDate { get; set; }

        public bool IsActive { get; set; }

    }
}