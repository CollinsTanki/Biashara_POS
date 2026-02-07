using System.ComponentModel.DataAnnotations;

namespace Biashara_POS.Models
{
    public class AppFunction
    {
        public int AppFunctionId { get; set; }

        [Required]
        public string FunctionName { get; set; } = "";// Post Order, Print KOT

        // FK
        public int ModuleId { get; set; }

        // Navigation
        public Module Module { get; set; } 

    }
}
