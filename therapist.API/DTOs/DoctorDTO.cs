using System.ComponentModel.DataAnnotations;

namespace therapist.API.DTOs
{
    public class DoctorDTO
    {
        [Required]
        public string Specially { get; set; }
        [Required]
        public string Activity_status { get; set; }
        [Required]
        public IFormFile ImageUrl { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        public double Rate { get; set; }
    }
}
