using ExamProject.Application.DTOs.StudentDTOs.ExamDTOs;
using ExamProject.Application.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;

namespace ExamProject.API.Controllers {

    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase {
        private readonly IExamService _examService;
        private readonly IUserExamService _userExamService;

        public StudentController(IExamService examService, IUserExamService userExamService) {
            _examService = examService;
            _userExamService = userExamService;
        }

        [HttpGet("exams/{userId}")]
        public IActionResult GetExams(int userId) {
            try {
                var exams = _userExamService.GetUnpassedUserExamsForUser(userId);
                return Ok(exams);
            } catch (Exception ex) {
                return BadRequest();
            }
        }

        [HttpGet("examQuestions/{examId}")]
        public async Task<IActionResult> GetExamQuestions(int examId) {
            var questions = await _examService.GetExamQuestionsAsync(examId);
            return Ok(questions);
        }

        [HttpPost("submitExam")]    
        public async Task<IActionResult> SubmitExam([FromBody] SubmitAnswerDTO model) {
            var result = await _examService.SubmitExamAsync(model);
            return Ok(result);
        }

        [HttpGet("examDetails/{examId}")]
        public async Task<IActionResult> GetExamDetails(int examId) {
            var exam = await _examService.GetExamWithQuestionsAsync(examId);
            return Ok(exam);
        }

        [HttpGet("{userId}/PassedExams")]
        public IActionResult GetPassedExams(int userId) {
            try {
                var completedExams = _userExamService.GetPassedExamsAsync(userId);
                return Ok(completedExams);
            } catch (Exception ex) {
                return BadRequest();
            }
        }

        [HttpGet("{userId}/PassedExams/{examId}")]
        public IActionResult GetPassedExams(int userId, int examId) {
            try {
                var ExamDelails = _examService.GetExamDetails(userId, examId);
                return Ok(ExamDelails);
            } catch (Exception ex) {
                return BadRequest();
            }
        }

        [HttpGet("AllUserExams")]
        public IActionResult GetAllUserExams()
        {
            try
            {
                var completedExams = _userExamService.GetAllExamsForUser();
                return Ok(completedExams);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

    }
}