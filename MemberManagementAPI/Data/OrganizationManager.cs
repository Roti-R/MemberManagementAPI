using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MemberManagementAPI.Data
{
    [Table("Management")]
    public class OrganizationManager
    {
        public Guid ManagerID { get; set; }
        public Guid OrgID { get; set; }

        //Relationship
        public Organization Organization { get; set; }
        public Member Manager { get; set; }
    }
}
