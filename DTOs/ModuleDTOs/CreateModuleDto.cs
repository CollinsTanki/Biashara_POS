using System.ComponentModel.DataAnnotations;

namespace Biashara_POS.DTOs.ModuleDTOs
{
    public class CreateModuleDto
    {
        [Required]
        [MaxLength(100)]
        public string ModuleName { get; set; } = "";
    }
}