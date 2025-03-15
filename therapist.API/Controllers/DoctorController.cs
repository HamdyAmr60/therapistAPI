using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using therapist.API.Attributes;
using therapist.API.DTOs;
using therapist.API.Helpers;
using Therapist.Core;
using Therapist.Core.Models;
using Therapist.Core.Models.Identity;
using Therapist.Core.Reposatories;

namespace therapist.API.Controllers
{

    public class DoctorController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;

        public DoctorController(IUnitOfWork unitOfWork , IMapper mapper, UserManager<AppUser> userManager)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
            this._userManager = userManager;
        }
        [Cache(300)]
        [HttpGet]
        public async Task<ActionResult<Doctor>> GetAllDoctors()
        {
        var result =   await  _unitOfWork.repo<Doctor>().GetAllAsync();
            return Ok(result);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> AddDoctor(DoctorDTO doctorDTO)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var user = _userManager.GetUserId(User);
          var mappedImage =   WorkWithImages.UploadImages(doctorDTO.ImageUrl, "DoctorsImage");
            var Mapped = _mapper.Map<Doctor>(doctorDTO);
            Mapped.ImageUrl = mappedImage;
            Mapped.UserId = user;

           await _unitOfWork.repo<Doctor>().AddAsync(Mapped);
           await  _unitOfWork.SavaAsync();
          await  _unitOfWork.DisposeAsync();
            return Ok(new { msg = "Doctor Added Succfully" });
        }
    }
}
