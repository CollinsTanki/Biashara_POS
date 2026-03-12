namespace Biashara_POS.DTOs.Users
{
    public class UserIndexDto
    {
        public string Id { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? Position { get; set; }
        public bool IsActive { get; set; }
        public string Branch { get; set; } = string.Empty; // display name only
    }
}