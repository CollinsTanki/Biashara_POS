namespace Biashara_POS.DTOs.Users
{
    public class UserIndexDto
    {
        public string Id { get; set; } = "";

        public string FullName { get; set; } = "";

        public string Email { get; set; } = "";

        public string Position { get; set; } = "";

        public string Branch { get; set; } = "";

        public bool IsActive { get; set; }
    }
}