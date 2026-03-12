using System.ComponentModel.DataAnnotations;

namespace Biashara_POS.DTOs.GroupPermission

{
    public class UpdateGroupPermissionDto
    {
        public int GroupPermissionId { get; set; }

        [Required]
        public int UserGroupId { get; set; }

        [Required]
        public int AppFunctionId { get; set; }

        public bool CanView { get; set; }

        public bool CanCreate { get; set; }

        public bool CanEdit { get; set; }

        public bool CanDelete { get; set; }
    }
}