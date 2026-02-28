using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Biashara_POS.Models
{
    public class Expense
    {
        [Key]
        public int ExpenseId { get; set; }

        [Required(ErrorMessage = "Expense name is required")]
        [MaxLength(200)]
        public string ExpenseName { get; set; } = "";

        [Required(ErrorMessage = "Amount is required")]
        [Column(TypeName = "decimal(18,2)")] // Fix EF Core warning about decimal precision
        [Range(0, double.MaxValue, ErrorMessage = "Amount must be positive")]
        public decimal Amount { get; set; }

        [Required]
        public DateTime ExpenseDate { get; set; } = DateTime.Now;

        public int? DepartmentId { get; set; }

        [Required]
        [MaxLength(450)] // Assuming ASP.NET Identity User Id max length
        public string RecordedByUserId { get; set; } = "";
    }
}