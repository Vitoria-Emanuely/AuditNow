#region Using
using System.ComponentModel.DataAnnotations.Schema;
#endregion

namespace AuditNow.Core.Models.ValueObjects
{
    [NotMapped]
    public class JwtToken
    {

        public string Token { get; set; }

        public DateTime Expiration { get; set; }

    }
}