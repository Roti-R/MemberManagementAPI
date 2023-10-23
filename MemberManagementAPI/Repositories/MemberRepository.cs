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
            return _context.Members.ToList();
        }

        public bool CreateMember(Member memberCreate)
        {
            _context.Members.Add(memberCreate);
            return Save();
        }

        public bool DeleteMember(Guid memberDelete)
        {
            throw new NotImplementedException();
        }

        public ICollection<Member> GetMember(Guid memberId)
        {
            throw new NotImplementedException();
        }

        public bool MemberExists(Guid memberId)
        {
            throw new NotImplementedException();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
