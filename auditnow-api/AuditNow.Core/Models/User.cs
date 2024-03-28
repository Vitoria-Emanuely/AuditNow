#region Using
using AuditNow.Core.Models.ValueObjects;
#endregion

namespace AuditNow.Core.Models
{
    public class User : BaseEntity
    {

        public int UserId { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public DateTime LastAccessDate { get; set; }

        public JwtToken JwtToken { get; set; }

    }
}