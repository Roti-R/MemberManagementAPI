using MemberManagementAPI.Data;

namespace MemberManagementAPI.Interfaces
{
    public interface IMemberRepository
    {
        ICollection<Member> GetAllMembers();

        Member GetMember(Guid memberId);

        Organization GetOrganizationOfMember(Guid memberId);

        bool MemberExists(Guid memberId);

        bool CreateMember(Member memberCreate);

        bool DeleteMember(Member memberDelete);

        ICollection<Member> RemoveAdmin(ICollection<Member> MemberList);
        bool Save();


    }
}
