using System.ComponentModel.DataAnnotations;

namespace Biashara_POS.DTOs.Users
{
    public class UserCreateDto
    {
        [Required]
        public string FullName { get; set; } = "";

        [Required]
        [EmailAddress]
        public string Email { get; set; } = "";

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = "";

        public string? Position { get; set; }

        public string? Address { get; set; }

        public int BranchId { get; set; }

        public int UserGroupId { get; set; }

        public bool IsActive { get; set; } = true;
    }
}