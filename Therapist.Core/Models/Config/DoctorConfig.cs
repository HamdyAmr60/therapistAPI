using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Therapist.Core.Models.Config
{
    public class DoctorConfig : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            builder.Property(p => p.Specially).HasMaxLength(100);
            builder.Property(p => p.Activity_status).HasMaxLength(50);
            builder.Property(p => p.Code).HasMaxLength(50);
            builder.Property(p => p.Rate).HasColumnType("decimal(18.2)");
        }
    }
}
