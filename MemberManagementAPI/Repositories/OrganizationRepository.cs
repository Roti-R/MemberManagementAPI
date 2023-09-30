using MemberManagementAPI.Data;
using MemberManagementAPI.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MemberManagementAPI.Repositories
{
    public class OrganizationRepository : IOrganizationRepository
    {
        private readonly MyDBContext _context;
        public OrganizationRepository(MyDBContext context)
        {
            _context = context;
        }

        public ICollection<Organization> GetAllOrganizations()
        {
            return _context.Organizations.ToList();
        }

        public ICollection<Organization> GetOrganization(string type)
        {
            return _context.Organizations.Where(org =>  org.Type == type).ToList();
        }
        public Organization GetOrganization(Guid orgId)
        {
            return _context.Organizations.Where(org => org.OrgID == orgId).FirstOrDefault();
        }



        public bool OrganizationExists(Guid orgId)
        {
            throw new NotImplementedException();
        }


        public bool CreateOrganization(Organization organizationCreate)
        {
            var rootOrg = _context.Organizations.Where(organization => organization.Type == "root").FirstOrDefault();
            if(organizationCreate.Type == "tinh") { organizationCreate.ParentOrganization = rootOrg; }
            _context.Add(organizationCreate);
            return Save();

        }


        public bool DeleteOrganizationAndChild(Organization organization)
        {
            var childs = _context.Organizations.Where(org => org.ParentOrganization == organization).ToList();
            foreach (var child in childs)
            {
                DeleteOrganizationAndChild(child);
            }
            _context.Organizations.Remove(organization);
            return Save();
        }


        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }


    }
}
