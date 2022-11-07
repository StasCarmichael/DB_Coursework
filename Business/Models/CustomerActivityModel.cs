using System;

namespace Business.Models
{
    public class CustomerActivityModel : BaseModel
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public decimal ReceiptSum { get; set; }


        public override bool Equals(object obj)
        {
            if (obj is CustomerActivityModel model && model.CustomerId == this.CustomerId 
                && model.CustomerName == this.CustomerName && model.ReceiptSum == this.ReceiptSum)
                return true;      
            else return false;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(Id, CustomerId, CustomerName, ReceiptSum);
        }
    }
}
