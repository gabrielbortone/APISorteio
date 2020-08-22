using System.ComponentModel.DataAnnotations;

namespace APISorteio.DTOs
{
    public class ForgotPasswordDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
