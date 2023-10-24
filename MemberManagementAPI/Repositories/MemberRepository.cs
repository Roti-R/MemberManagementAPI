using MemberManagementAPI.Data;
using MemberManagementAPI.Interfaces;

namespace MemberManagementAPI.Repositories
{
    public class MemberRepository : IMemberRepository
    {
        private readonly MyDBContext _context;

        public MemberRepository(MyDBContext context)
        {
            _context = context;
        }
        public ICollection<Member> GetAllMembers()
        {
            return RemoveAdmin(_context.Members.ToList());
        }

        public bool CreateMember(Member memberCreate)
        {
            _context.Members.Add(memberCreate);
            return Save();
        }

        public bool DeleteMember(Member memberDelete)
        {
            _context.Members.Remove(memberDelete);
            return Save();
        }

        public Member GetMember(Guid memberId)
        {
            return _context.Members.Where(member => member.MemberID == memberId).FirstOrDefault();
        }
        public bool MemberExists(Guid memberId)
        {
            return _context.Members.Any(member => member.MemberID == memberId);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public Organization GetOrganizationOfMember(Guid memberId)
        {
            var member = this.GetMember(memberId);
            if(member.CurrentOrganizationID == null) { return null; }
            return _context.Organizations.Where(org => org.OrgID == member.CurrentOrganizationID).FirstOrDefault();
        }

        public ICollection<Member> RemoveAdmin(ICollection<Member> MemberList)
        {
            Guid adminGuid = new Guid("92E8C2B2-97D9-4D6D-A9B7-48CB0D039A84");
            var admin = this.GetMember(adminGuid);
            MemberList.Remove(admin);
            return MemberList;

        }
    }
}
