using System.ComponentModel.DataAnnotations;

namespace Biashara_POS.Models
{
    public class ImportLog
    {
        [Key]
        public int ImportLogId { get; set; }

        [Required]
        [MaxLength(50)]
        public string EntityName { get; set; } = "";
        // Customer, Supplier, Product, Staff

        [Required]
        public int TotalRecords { get; set; }

        public int SuccessfulRecords { get; set; }

        public int FailedRecords { get; set; }

        [Required]
        public DateTime ImportDate { get; set; } = DateTime.Now;

        [Required]
        public string ImportedByUserId { get; set; } = "";

        // Navigation
        public AppUser ImportedByUser { get; set; } = null!;
    }
}
