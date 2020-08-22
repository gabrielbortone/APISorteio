namespace APISorteio.DTOs
{
    public class AdministradorDTO
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string PhoneNumber { get; set; }
        public bool RememberMe { get; set; }
    }
}
