using ExamManagement.Business.Abstract;
using ExamManagement.Business.DTOs.SubjectDTOs;
using ExamManagement.Business.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ExamManagement.Business.DTOs.StudentExamDTOs;

namespace ExamManagement.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize("Admin")]
    public class StudentExamsController : ControllerBase
    {
        private readonly IStudentExamService _studentExamService;
        public StudentExamsController(IStudentExamService studentExamService)
        {
            _studentExamService = studentExamService;
        }
        [HttpGet]
        public IActionResult Get([FromQuery] PaginationDTO paginationDTO)
        {
            var values = _studentExamService.GetAll(paginationDTO.PageNumber, paginationDTO.PageSize);

            return Ok(values);
        }


        [HttpPost("[action]")]
        public IActionResult Create(CreateStudentExamDTO studentExamDTO)
        {

            _studentExamService.Add(studentExamDTO);

            return Ok(new { Message = "Subject added", StatusCode = 200 });
        }

        [HttpGet("[action]/{id}")]
        public IActionResult Deactive(int id)
        {
            _studentExamService.Deactive(id);

            return Ok(new { StatusCode = 200, Message = "Subject deleted" });
        }

        [HttpPut("[action]")]
        public IActionResult Update(UpdateStudenExamDTO studenExamDTO)
        {
            _studentExamService.Update(studenExamDTO);

            return Ok(new { StatusCode = 200 });
        }
    }
}
