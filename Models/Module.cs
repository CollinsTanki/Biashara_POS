using System.ComponentModel.DataAnnotations;

namespace Biashara_POS.Models
{
    public class Module
    {
        [Key]
        public int ModuleId { get; set; }

        // --------------------
        // REQUIRED COLUMN
        // --------------------
        [Required]
        [MaxLength(100)]
        public string ModuleName { get; set; } = string.Empty; // Sales, Inventory

        // --------------------
        // NAVIGATION PROPERTY
        // --------------------
        public ICollection<AppFunction> Functions { get; set; } = new List<AppFunction>();
    }
}
