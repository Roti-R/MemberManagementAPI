using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MemberManagementAPI.Data
{
    [Table("Account")]
    public class Account
    {
        [Key]
        public Guid MemberID { get; set; }
        [Required]
        [MaxLength(100)]
        public string Username { get; set; }
        [Required]
        [MaxLength(100)]
        public string Password { get; set; }

        //RELATIONSHIP
        public Member Member { get; set; }


    }
}
