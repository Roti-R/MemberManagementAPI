using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MemberManagementAPI.Data
{
    [Table("Member")]
    public class Member
    {
        [Key]
        public Guid MemberID { get; set; }
        [Required]
        public string Name { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime JoinDate { get; set; }
        public string? Address { get; set; }

        //relationship 
        public Account? Account { get; set; }    

        
        public ICollection<OrganizationManager>? OrganizationManagers { get; set; }


        public Organization Organization { get; set; }
        public Guid CurrentOrganizationID { get; set; }
    }
}
