namespace Data.Entities
{
    public class ReceiptDetail : BaseEntity
    {
        public int ReceiptId { get; set; }
        public int ProductId { get; set; }
        public decimal DiscountUnitPrice { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }


        // Navigation properties
        public virtual Receipt Receipt { get; set; }
        public virtual Product Product { get; set; }
    }
}
