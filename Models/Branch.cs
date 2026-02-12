using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Biashara_POS.Models
{
    public class Branch
    {
        [Key]
        public int BranchId { get; set; }

        // --------------------
        // FOREIGN KEY
        // --------------------
        [Required]
        public int CompanyId { get; set; }

        // --------------------
        // NAVIGATION
        // --------------------
        [ForeignKey(nameof(CompanyId))]
        public Company Company { get; set; } = null!;

        // --------------------
        // PROPERTIES
        // --------------------
        [Required]
        public bool IsHQ { get; set; } = false;

        [Required]
        [MaxLength(150)]
        public string BranchName { get; set; } = string.Empty;

        [MaxLength(200)]
        public string Address { get; set; } = string.Empty;

        [MaxLength(20)]
        public string Phone { get; set; } = string.Empty;

        [MaxLength(150)]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [MaxLength(100)]
        public string Location { get; set; } = string.Empty;

        // --------------------
        // RELATIONSHIPS
        // --------------------
        public ICollection<AppUser> Users { get; set; } = new List<AppUser>();
    }
}
