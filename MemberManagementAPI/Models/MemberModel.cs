namespace MemberManagementAPI.Models
{
    public class MemberModel
    {
        public Guid MemberID { get; set; }
        public string Name { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime JoinDate { get; set; }
        public string? Address { get; set; }
    }
}
