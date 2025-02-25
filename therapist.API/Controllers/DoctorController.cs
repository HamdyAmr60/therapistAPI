using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using therapist.API.Attributes;
using Therapist.Core;
using Therapist.Core.Models;
using Therapist.Core.Reposatories;

namespace therapist.API.Controllers
{

    public class DoctorController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;

        public DoctorController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        [Cache(300)]
        [HttpGet]
        public async Task<ActionResult<Doctor>> GetAllDoctors()
        {
        var result =   await  _unitOfWork.repo<Doctor>().GetAllAsync();
            return Ok(result);
        }
    }
}
