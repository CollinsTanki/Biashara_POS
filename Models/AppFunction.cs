using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Biashara_POS.Models
{
    public class AppFunction
    {
        [Key]
        public int AppFunctionId { get; set; }

        // REQUIRED + LENGTH
        [Required]
        [MaxLength(100)]
        public string FunctionName { get; set; } = ""; // Post Order, Print KOT

        // FOREIGN KEY
        [Required]
        public int ModuleId { get; set; }

        // NAVIGATION
        [ForeignKey(nameof(ModuleId))]
        public Module Module { get; set; } = null!;

        // MANY-TO-MANY VIA GroupPermission
        public ICollection<GroupPermission> GroupPermissions { get; set; }
            = new List<GroupPermission>();


    }
}
