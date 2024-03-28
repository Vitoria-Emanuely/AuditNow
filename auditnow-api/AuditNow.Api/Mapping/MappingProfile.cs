#region Using
using AuditNow.Api.Resources.Transaction;
using AuditNow.Api.Resources.User;
using AuditNow.Core.Models;
using AuditNow.Core.Models.ValueObjects;
#endregion


namespace AuditNow.Api.Mapping
{
    public class MappingProfile : AutoMapper.Profile
    {
        public MappingProfile()
        {
            // Domain to Resource
            CreateMap<Transaction, TransactionResource>();
            CreateMap<User, UserResource>();
            CreateMap<JwtToken, JwtTokenResource>();


            // Resource to Domain
            CreateMap<TransactionResource, Transaction>();
            CreateMap<UserResource, User>();
            CreateMap<CreateUserResource, User>();
            CreateMap<JwtTokenResource, JwtToken>();

        }
    }
}