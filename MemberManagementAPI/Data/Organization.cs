using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MemberManagementAPI.Data
{
 
    [Table("Organization")]
    public class Organization
    {
        [Key]
        public Guid OrgID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Type { get; set; }

        //Relationship

        public ICollection<OrganizationManager>? OrganizationManagers { get; set; }


        public ICollection<Organization>? ChildOrganizations { get; set; }
        public Organization? ParentOrganization { get; set; }
        public Guid? ParentID { get; set; }


        public ICollection<Member>? Members { get; set; }    
    }
}
