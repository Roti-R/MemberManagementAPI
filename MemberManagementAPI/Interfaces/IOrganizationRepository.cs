using MemberManagementAPI.Data;

namespace MemberManagementAPI.Interfaces
{
    public interface IOrganizationRepository
    {
        ICollection<Organization> GetAllOrganizations();

        Organization GetParentOrganization(Guid orgId);

        ICollection<Organization> GetOrganization(string type);
        Organization GetOrganization(Guid orgId);

        ICollection<Member> GetAllMembers(Guid orgId);

        bool OrganizationExists(Guid orgId);



        bool CreateOrganization(Organization organizationCreate);

        ICollection<Organization> GetChildOrganization(Guid orgId);

        bool DeleteOrganizationAndChild(Organization organization);

        ICollection<Member> GetAllManager(Guid orgId);
        bool CreateManager(Guid memberID, Guid orgID );

        public bool DeleteManager(Guid memberID);

        bool Save();
    }
}
