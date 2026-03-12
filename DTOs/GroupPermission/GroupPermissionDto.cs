namespace Biashara_POS.DTOs.GroupPermission
{
    public class GroupPermissionDto
    {
        public int GroupPermissionId { get; set; }

        public int UserGroupId { get; set; }

        public string GroupName { get; set; } = "";

        public int AppFunctionId { get; set; }

        public string FunctionName { get; set; } = "";

        public bool CanView { get; set; }

        public bool CanCreate { get; set; }

        public bool CanEdit { get; set; }

        public bool CanDelete { get; set; }
    }
}