using System.ComponentModel.DataAnnotations;

namespace Biashara_POS.Models
{
    public class Module
    {
        public int ModuleId { get; set; }

        [Required]
        public string ModuleName { get; set; } = ""; // Sales, Inventory

        public ICollection<AppFunction> Functions { get; set; }

    }
}
