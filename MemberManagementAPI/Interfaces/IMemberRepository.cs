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
        bool UpdateMember(Member memberUpdate);
        public bool DeleteManagerFromOrganizationAndChild(Guid memberID);

        ICollection<Member> RemoveAdmin(ICollection<Member> MemberList);
        bool Save();


    }
}
