namespace Data.Entities
{
    public class Administrator : BaseEntity
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public int AccessLevel { get; set; }
    }
}
