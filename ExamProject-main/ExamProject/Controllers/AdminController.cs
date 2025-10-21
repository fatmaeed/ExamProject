using ExamProject.Application.DTOs.AdminDTOs.ExamStudentsDTOs;
using ExamProject.Application.DTOs.AdminDTOs.QuestionDTOs;
using ExamProject.Application.Interfaces.IServices;
using ExamProject.Application.Utils;
using Microsoft.AspNetCore.Mvc;

namespace ExamProject.API.Controllers {

    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase {
        private readonly IAdminService adminService;

        public AdminController(IAdminService adminService) {
            this.adminService = adminService;
        }

        [HttpGet("Exams/{id}/Students")]
        public async Task<IActionResult> GetAllStudentsByExam(int id) {
            Either<Failure, ExamStudentsDTO> result = await adminService.GetExamStudents(id);
            if (result.IsSuccess)
                return Ok(result.Right);
            else
                return FailureIActionResult.FailureHandler(result.Left);
        }

        [HttpPost("Exam/{id}")]
        public async Task<IActionResult> AddQuestion(int id, CreateQuestionDTO questionDTO) {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            Either<Failure, UpdateQuestionDTO> result = await adminService.CreateQuestion(id, questionDTO);
            if (result.IsSuccess)
                return CreatedAtAction(nameof(GetQuestion), new { id = result.Right.Id }, result.Right);
            else
                return FailureIActionResult.FailureHandler(result.Left);
        }

        [HttpGet("Exam/{id}/Questions")]
        public async Task<IActionResult> GetAllQuestions(int id, int page = 1, int pageSize = 10) {
            Either<Failure, List<DisplayQuestionDTO>> result = await adminService.GetAllQuestionsForExam(id, page, pageSize);
            if (result.IsSuccess)
                return Ok(result.Right);
            else
                return FailureIActionResult.FailureHandler(result.Left);
        }

        [HttpGet("Question/{id}")]
        public async Task<IActionResult> GetQuestion(int id) {
            Either<Failure, DisplayQuestionDTO> result = await adminService.GetQuestionById(id);
            if (result.IsSuccess)
                return Ok(result.Right);
            else
                return FailureIActionResult.FailureHandler(result.Left);
        }

        [HttpPut("Question/{id}")]
        public async Task<IActionResult> UpdateQuestion(int id, UpdateQuestionDTO questionDTO) {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            Either<Failure, UpdateQuestionDTO> result = await adminService.UpdateQuestion(id, questionDTO);
            if (result.IsSuccess)
                return NoContent();
            else
                return FailureIActionResult.FailureHandler(result.Left);
        }

        [HttpDelete("Question/{id}")]
        public async Task<IActionResult> DeleteQuestion(int id) {
            Either<Failure, BaseQuestionDTO> result = await adminService.DeleteQuestion(id);
            if (result.IsSuccess)
                return Ok(result.Right);
            else
                return FailureIActionResult.FailureHandler(result.Left);
        }

        [HttpGet("Exam/{id}/Questions/{search}")]
        public IActionResult SearchAboutQuestion(int id, string search, int page = 1, int pageSize = 10) {
            Either<Failure, List<DisplayQuestionDTO>> result = adminService.SearchQuestion(id, search, page, pageSize);
            if (result.IsSuccess)
                return Ok(result.Right);
            else
                return FailureIActionResult.FailureHandler(result.Left);
        }
    }
}