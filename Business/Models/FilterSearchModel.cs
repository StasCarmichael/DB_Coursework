namespace Business.Models
{
    public class FilterSearchModel
    {
        public int? CategoryId { get; set; } = null;
        public int? MinPrice { get; set; } = null;
        public int? MaxPrice { get; set; } = null; 
    }
}
