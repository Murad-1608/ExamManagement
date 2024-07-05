using ExamManagement.Business.Abstract;
using ExamManagement.Business.DTOs.SubjectDTOs;
using ExamManagement.Business.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ExamManagement.Business.DTOs.ExamDTOs;
using Microsoft.AspNetCore.Authorization;

namespace ExamManagement.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="Admin")]
    public class ExamsController : ControllerBase
    {
        private IExamService _examService;
        public ExamsController(IExamService examService)
        {
            _examService = examService;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] PaginationDTO paginationDTO)
        {
            var values = _examService.GetAll(paginationDTO.PageNumber, paginationDTO.PageSize);

            return Ok(values);
        }


        [HttpPost("[action]")]
        public IActionResult Create(CreateExamDTO examDTO)
        {
            _examService.Add(examDTO);

            return Ok(new { Message = "Exam added", StatusCode = 200 });
        }

        [HttpGet("[action]/{id}")]
        public IActionResult Deactive(int id)
        {
            _examService.Deactive(id);

            return Ok(new { StatusCode = 200, Message = "Exam deleted" });
        }

        [HttpPut("[action]")]
        public IActionResult Update(UpdateExamDTO examDTO)
        {
            _examService.Update(examDTO);

            return Ok(new { StatusCode = 200 });
        }

    }
}
