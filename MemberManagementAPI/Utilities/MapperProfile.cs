using AutoMapper;
using MemberManagementAPI.Data;
using MemberManagementAPI.Models;

namespace MemberManagementAPI.Utilities
{
    public class MapperProfile: Profile
    {
        public MapperProfile()
        {
            CreateMap<Account, AccountModel>();
            CreateMap<Organization, OrganizationModel>();
            CreateMap<OrganizationModel, Organization>();
        }

    }
}
