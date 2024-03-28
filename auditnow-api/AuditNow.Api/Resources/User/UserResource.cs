namespace AuditNow.Api.Resources.User
{
    public class UserResource
    {

        public int UserId { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public bool IsActive { get; set; }

        public DateTime? LastAccessDate { get; set; }

        public JwtTokenResource JwtToken { get; set; }

    }
}