using System.ComponentModel.DataAnnotations;

namespace Biashara_POS.DTOs.AppFunction
{
    public class UpdateAppFunctionDto
    {
        public int AppFunctionId { get; set; }

        [Required]
        [MaxLength(100)]
        public string FunctionName { get; set; } = "";

        [Required]
        public int ModuleId { get; set; }
    }
}