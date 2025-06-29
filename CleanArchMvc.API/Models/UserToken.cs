namespace CleanArchMvc.API.Models
{
    public class UserToken
    {
        public string Token = string.Empty;
        public DateTime Experation { get; set; }

    }
}
