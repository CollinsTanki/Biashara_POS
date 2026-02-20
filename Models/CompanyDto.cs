using System.ComponentModel.DataAnnotations;

public class CompanyDto
{
    [Required]
    [MaxLength(150)]
    public string CompanyName { get; set; } = string.Empty;

    [Required]
    [MaxLength(50)]
    public string RegistrationType { get; set; } = string.Empty;

    [MaxLength(20)]
    public string KRAPin { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [MaxLength(150)]
    public string Email { get; set; } = string.Empty;

    [Required]
    [MaxLength(20)]
    public string Phone { get; set; } = string.Empty;

    public IFormFile? ImageFile { get; set; }
}
