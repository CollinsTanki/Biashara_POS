using System.ComponentModel.DataAnnotations;

namespace Biashara_POS.DTOs.Users
{
    public class UserEditDto
    {
        public string Id { get; set; } = "";

        [Required]
        public string FullName { get; set; } = "";

        [EmailAddress]
        public string Email { get; set; } = "";

        public string? Position { get; set; }

        public string? Address { get; set; }

        public int BranchId { get; set; }

        public int UserGroupId { get; set; }

        public bool IsActive { get; set; }
    }
}