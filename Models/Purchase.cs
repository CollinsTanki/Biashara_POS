using Biashara_POS.Models;
using System.ComponentModel.DataAnnotations;

public class Purchase
{
    public int PurchaseId { get; set; }

    [Required]
    public DateTime PurchaseDate { get; set; } = DateTime.Now;

    [Required]
    public int SupplierId { get; set; }

    [Required]
    [Range(0, double.MaxValue)]
    public decimal TotalAmount { get; set; }

    public bool IsCredit { get; set; } = false;

    public Supplier Supplier { get; set; }

    public ICollection<PurchaseItem> PurchaseItems { get; set; } = new List<PurchaseItem>();
}