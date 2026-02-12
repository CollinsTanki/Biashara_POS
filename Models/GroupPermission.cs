using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Biashara_POS.Models
{
    public class GroupPermission
    {
        [Key]
        public int GroupPermissionId { get; set; }

        // --------------------
        // FOREIGN KEYS
        // --------------------
        [Required]
        public int UserGroupId { get; set; }

        [Required]
        public int AppFunctionId { get; set; }

        // --------------------
        // PERMISSIONS
        // --------------------
        public bool CanView { get; set; } = false;
        public bool CanCreate { get; set; } = false;
        public bool CanEdit { get; set; } = false;
        public bool CanDelete { get; set; } = false;

        // --------------------
        // NAVIGATION
        // --------------------
        [ForeignKey(nameof(UserGroupId))]
        public UserGroup UserGroup { get; set; } = null!;

        [ForeignKey(nameof(AppFunctionId))]
        public AppFunction AppFunction { get; set; } = null!;
    }
}
