using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.ComponentModel.DataAnnotations;

namespace Biashara_POS.Models
{
    public class Branch
    {
        public int BranchId { get; set; }

        public int CompanyId { get; set; }

        public bool IsHQ { get; set; }

        [Required]
        public string BranchName { get; set; } = "";

        public string Address { get; set; } = "";
        public string Phone { get; set; } = "";
        public string Email { get; set; } = "";
        public string Location { get; set; } = "";

        public Company Company { get; set; }
        public ICollection<User> Users { get; set; }

    }
}
