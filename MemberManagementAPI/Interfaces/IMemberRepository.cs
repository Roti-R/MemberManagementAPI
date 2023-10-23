using MemberManagementAPI.Data;

namespace MemberManagementAPI.Interfaces
{
    public interface IMemberRepository
    {
        ICollection<Member> GetAllMembers();

        ICollection<Member> GetMember(Guid memberId);

        bool MemberExists(Guid memberId);

        bool CreateMember(Member memberCreate);

        bool DeleteMember(Guid memberDelete);

        bool Save();


    }
}
