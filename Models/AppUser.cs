using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Biashara_POS.Models
{
    public class AppUser : IdentityUser
    {
        public bool IsActive { get; set; } = true;

        // ---------- Foreign Keys ----------  
        public int UserGroupId { get; set; }
        public int BranchId { get; set; }

        // ---------- Navigation ----------  
        [ForeignKey(nameof(UserGroupId))]
        public UserGroup? UserGroup { get; set; }

        [ForeignKey(nameof(BranchId))]
        public Branch? Branch { get; set; }
    }
}
