using System.ComponentModel.DataAnnotations;

namespace Biashara_POS.DTOs.AppFunction
{
    public class CreateAppFunctionDto
    {
        [Required]
        [MaxLength(100)]
        public string FunctionName { get; set; } = "";

        [Required]
        public int ModuleId { get; set; }
    }
}