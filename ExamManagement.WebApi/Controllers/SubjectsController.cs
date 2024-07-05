using ExamManagement.Business.Abstract;
using ExamManagement.Business.DTOs.TeacherDTOs;
using ExamManagement.Business.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ExamManagement.Business.DTOs.StudentDTOs;
using ExamManagement.Business.DTOs.SubjectDTOs;

namespace ExamManagement.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectsController : ControllerBase
    {
        private readonly ISubjectService _subjectService;
        public SubjectsController(ISubjectService subjectService)
        {
            _subjectService = subjectService;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] PaginationDTO paginationDTO)
        {
            var values = _subjectService.GetAll(paginationDTO.PageNumber, paginationDTO.PageSize);

            return Ok(values);
        }


        [HttpPost("[action]")]
        public IActionResult Create(CreateSubjectDTO subjectDTO)
        {

            _subjectService.Add(subjectDTO);

            return Ok(new { Message = "Subject added", StatusCode = 200 });
        }

        [HttpGet("[action]/{id}")]
        public IActionResult Deactive(int id)
        {
            _subjectService.Deactive(id);

            return Ok(new { StatusCode = 200, Message = "Subject deleted" });
        }

        [HttpPut("[action]")]
        public IActionResult Update(UpdateSubjectDTO subjectDTO)
        {
            _subjectService.Update(subjectDTO);

            return Ok(new { StatusCode = 200 });
        }
    }
}
