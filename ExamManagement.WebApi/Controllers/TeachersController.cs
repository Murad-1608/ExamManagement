using Entity.Concrete.Identity;
using ExamManagement.Business.Abstract;
using ExamManagement.Business.DTOs.StudentDTOs;
using ExamManagement.Business.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ExamManagement.Business.DTOs.TeacherDTOs;
using Microsoft.AspNetCore.Authorization;

namespace ExamManagement.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeachersController : ControllerBase
    {
        private readonly ITeacherService _teacherService;

        public TeachersController(ITeacherService teacherService)
        {
            _teacherService = teacherService;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Get([FromQuery] PaginationDTO paginationDTO)
        {
            var values = _teacherService.GetAll(paginationDTO.PageNumber, paginationDTO.PageSize);

            return Ok(values);
        }


        [HttpPost("[action]")]
        public async Task<IActionResult> Create(CreateTeacherDTO teacherDTO)
        {

            await _teacherService.Add(teacherDTO);

            return Ok(new { Message = "Teacher added", StatusCode = 200 });
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("[action]/{id}")]
        public IActionResult Deactive(int id)
        {
            _teacherService.Deactive(id);

            return Ok(new { StatusCode = 200, Message = "Student deleted" });
        }

        [Authorize(Roles = "Admin,Teacher")]
        [HttpPut("[action]")]
        public async Task<IActionResult> Update(UpdateTeacherDTO teacherDTO)
        {
            await _teacherService.Update(teacherDTO);

            return Ok(new { StatusCode = 200 });
        }
    }
}
