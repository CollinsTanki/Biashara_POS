using System.ComponentModel.DataAnnotations;

namespace Biashara_POS.DTOs.UserGroup
{
    public class UpdateUserGroupDto
    {
        public int UserGroupId { get; set; }

        [Required]
        [MaxLength(50)]
        public string GroupName { get; set; } = "";

        [MaxLength(200)]
        public string Description { get; set; } = "";
    }
}