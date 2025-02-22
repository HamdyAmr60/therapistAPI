using System.ComponentModel.DataAnnotations;

namespace therapist.API.DTOs
{
    public class ForgetPasswordDTO
    {
        [Required, EmailAddress]
        public string Email { get; set; }

        [Required] // URL of the frontend reset page
        public string ClientURL { get; set; }
    }
}
