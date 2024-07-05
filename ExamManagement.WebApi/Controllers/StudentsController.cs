using Entity.Concrete.Identity;
using ExamManagement.Business.Abstract;
using ExamManagement.Business.DTOs;
using ExamManagement.Business.DTOs.StudentDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ExamManagement.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _studentService;
        public StudentsController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Get([FromQuery] PaginationDTO paginationDTO)
        {
            var values = _studentService.GetAll(paginationDTO.PageNumber, paginationDTO.PageSize);

            return Ok(values);
        }


        [HttpPost("[action]")]
        public async Task<IActionResult> Create(CreateStudentDTO studentDTO)
        {

            await _studentService.Add(studentDTO);

            return Ok(new { Message = "Student added", StatusCode = 200 });
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("[action]/{id}")]
        public IActionResult Deactive(int id)
        {
            _studentService.Deactive(id);

            return Ok(new { StatusCode = 200, Message = "Student deleted" });
        }

        [Authorize(Roles = "Admin,Student")]
        [HttpPut("[action]")]
        public async Task<IActionResult> Update(UpdateStudentDTO studentDTO)
        {
            await _studentService.Update(studentDTO);

            return Ok(new { StatusCode = 200 });
        }
    }
}
