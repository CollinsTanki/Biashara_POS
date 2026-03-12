namespace Biashara_POS.DTOs.UserGroup
{
    public class UserGroupDto
    {
        public int UserGroupId { get; set; }

        public string GroupName { get; set; } = "";

        public string Description { get; set; } = "";

        public bool IsEditable { get; set; }

        public int UserCount { get; set; }

        public int PermissionCount { get; set; }
    }
}
