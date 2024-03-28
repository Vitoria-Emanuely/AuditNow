namespace AuditNow.Api.Resources.User
{
    public class JwtTokenResource
    {

        public string Token { get; set; }

        public DateTime Expiration { get; set; }

    }
}
