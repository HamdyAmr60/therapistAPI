using AutoMapper;
using therapist.API.DTOs;
using Therapist.Core.Models;

namespace therapist.API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<DoctorDTO , Doctor>().ReverseMap();
        }
    }
}
