using MemberManagementAPI.Data;

namespace MemberManagementAPI.Interfaces
{
    public interface IOrganizationRepository
    {
        ICollection<Organization> GetAllOrganizations();

        ICollection<Organization> GetOrganizations(Guid parentId);

        ICollection<Organization> GetOrganization(string type);
        Organization GetOrganization(Guid orgId);

        ICollection<Member> GetAllMembers(Guid orgId);

        bool OrganizationExists(Guid orgId);



        bool CreateOrganization(Organization organizationCreate);



        bool DeleteOrganizationAndChild(Organization organization);



        bool Save();
    }
}
