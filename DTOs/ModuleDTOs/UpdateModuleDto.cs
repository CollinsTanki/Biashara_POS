using System.ComponentModel.DataAnnotations;

namespace Biashara_POS.DTOs.ModuleDTOs
{
    public class UpdateModuleDto
    {
        public int ModuleId { get; set; }

        [Required]
        [MaxLength(100)]
        public string ModuleName { get; set; } = "";
    }
}