namespace Business.Models
{
    public class ReceiptDetailModel : BaseModel
    {
        public int ReceiptId { get; set; }
        public int ProductId { get; set; }
        public decimal DiscountUnitPrice { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
    }
}
