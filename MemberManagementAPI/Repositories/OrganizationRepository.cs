using MemberManagementAPI.Data;
using MemberManagementAPI.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MemberManagementAPI.Repositories
{
    public class OrganizationRepository : IOrganizationRepository
    {
        private readonly MyDBContext _context;
        private readonly IMemberRepository _memberRepository;

        public OrganizationRepository(MyDBContext context, IMemberRepository memberRepository)
        {
            _context = context;
            _memberRepository = memberRepository;
        }

        public ICollection<Organization> GetAllOrganizations()
        {
            return _context.Organizations.ToList();
        }
       
        public Organization GetParentOrganization(Guid orgId)
        {

            var organization = GetOrganization(orgId);

            if(organization.ParentOrganization != null)
            {
                return organization.ParentOrganization;
            }
            return null;
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
            return _context.Organizations.Any(org => org.OrgID == orgId);
        }

        public bool ManagementExist(Member manager, Organization org) {
            return _context.OrganizationManagers.Any(m =>  (m.Manager == manager && m.Organization == org));
        }

        public bool CreateOrganization(Organization organizationCreate)
        {
            var rootOrg = _context.Organizations.Where(organization => organization.Type == "root").FirstOrDefault();
            if (organizationCreate.Type == "tinh") { organizationCreate.ParentOrganization = rootOrg; }
            _context.Add(organizationCreate);
            var parentOrg = organizationCreate.ParentOrganization;
            while(parentOrg != null)
            {
                foreach (var manager in GetAllManager(parentOrg.OrgID))
                {
                    if(!ManagementExist(manager, organizationCreate))
                    {
                        var managerOfOrganizationCreate = new OrganizationManager
                        {
                            Manager = manager,
                            Organization = organizationCreate,
                        };
                        _context.Entry(managerOfOrganizationCreate).State = EntityState.Detached;
                        _context.Add(managerOfOrganizationCreate);
                        if(parentOrg.ParentOrganization == null)
                        {
                            return Save();
                        }    
                        Save();
                       
                        
                    }    
                    
                    
                    
                }    
                parentOrg = parentOrg.ParentOrganization;

            }    

            
            return Save();

        }


        public bool DeleteOrganizationAndChild(Organization organization)
        {
            var childs = _context.Organizations.Where(org => org.ParentOrganization == organization).ToList();
            foreach (var child in childs)
            {
                DeleteOrganizationAndChild(child);
            }
            foreach(Member member in GetAllMembers(organization.OrgID))
            {
                member.CurrentOrganizationID = null;
            }    
            _context.Organizations.Remove(organization);
            return Save();
        }


        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public ICollection<Member> GetAllMembers(Guid orgId)
        {
            return _context.Members.Where(m => m.CurrentOrganizationID == orgId).ToList();
        }

        public ICollection<Organization> GetChildOrganization(Guid orgId)
        {
            return _context.Organizations.Where(o => o.ParentID == orgId).ToList();
        }

        public bool CreateManager(Guid memberID, Guid orgID)
        {
            var organization = GetOrganization(orgID);
            var member = _memberRepository.GetMember(memberID);

            foreach (var childOrg in GetChildOrganization(orgID))
            {
                CreateManager(memberID, childOrg.OrgID);
            }
            var organizationManager = new OrganizationManager()
            {
                Organization = organization,
                Manager = member
            };

            _context.Add(organizationManager);

            return Save();

           
        }

        public bool DeleteManager(Guid memberID)
        {
            _context.OrganizationManagers.RemoveRange(_context.OrganizationManagers.Where(m => m.ManagerID == memberID));
            return Save();
        }

        public ICollection<Member> GetAllManager(Guid orgId)
        {
            return _context.OrganizationManagers.Where(m => m.OrgID == orgId).Select(m => m.Manager).ToList();
        }
    }
}
