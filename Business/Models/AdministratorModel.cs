namespace Business.Models
{
    public class AdministratorModel : BaseModel
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public int AccessLevel { get; set; }
    }
}
