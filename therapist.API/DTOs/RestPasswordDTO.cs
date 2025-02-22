using System.ComponentModel.DataAnnotations;

namespace therapist.API.DTOs
{
    public class RestPasswordDTO
    {
        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Token { get; set; }

        [Required, MinLength(6)]
        public string NewPassword { get; set; }
    }
}
