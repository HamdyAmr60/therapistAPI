using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Therapist.Core.Models.Identity;

namespace Therapist.Core.Models
{
    public class Doctor:BaseEntity
    {
        public string Specially { get; set; }
        public string Activity_status { get; set; }
        public string ImageUrl { get; set; }
        public string Code { get; set; }

        public double Rate { get; set; }

        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual AppUser User { get; set; }

    }
}
