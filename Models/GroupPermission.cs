namespace Biashara_POS.Models
{
    public class GroupPermission
    {
        public int GroupPermissionId { get; set; }

        public int UserGroupId { get; set; }
        public int AppFunctionId { get; set; }

        public bool CanView { get; set; }
        public bool CanCreate { get; set; }
        public bool CanEdit { get; set; }
        public bool CanDelete { get; set; }

        public UserGroup UserGroup { get; set; }
        public AppFunction AppFunction { get; set; }

    }
}
