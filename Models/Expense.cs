using System.ComponentModel.DataAnnotations;

namespace Biashara_POS.Models
{
    public class Expense
    {
        public int ExpenseId { get; set; }

        [Required]
        public string ExpenseName { get; set; } = "";

        public decimal Amount { get; set; }

        public DateTime ExpenseDate { get; set; } = DateTime.Now;

        public int? DepartmentId { get; set; }

        public string RecordedByUserId { get; set; } = "";
    }
}
