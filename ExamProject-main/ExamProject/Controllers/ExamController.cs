using ExamProject.Application.DTOs.AdminDTOs.ExamDTOs;
using ExamProject.Application.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;

namespace ExamProject.API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class ExamController : ControllerBase {
        private readonly IExamService service;

        public ExamController(IExamService service) {
            this.service = service;
        }

        //get AllExam => name/min,max degree / duration /start,end Time
        [HttpGet]
        public async Task<IActionResult> GetAllExam() {
            return Ok(await service.GetAllExamAsync());
        }

        //get Exam => name/min,max degree / duration /start,end Time
        [HttpGet("Id")]
        public async Task<IActionResult> GetExamById(int examId) {
            if (examId <= 0) return BadRequest();
            try {
                var exam = await service.GetExamByIdAsync(examId);
                if (exam == null) return NotFound("Not Found Exam");
                return Ok(exam);
            } catch (Exception ex) {
                return BadRequest();
            }
        }

        //get Exam with Questions
        [HttpGet("ExamWithQuestions")]
        public async Task<IActionResult> GetExamWithQuestions(int id) {
            if (id <= 0) return BadRequest();
            try {
                var examWithQuest = await service.GetExamWithQuestionsAsync(id);
                if (examWithQuest == null) return NotFound("Exam not found.");
                return Ok(examWithQuest);
            } catch (Exception ex) {
                return BadRequest();
            }
        }

        [HttpGet("Search/ExamName")]
        public async Task<IActionResult> Search(string examName) {
            try {
                if (examName == null) return BadRequest();
                var result = await service.SearchAsync(examName);
                return Ok(result);
            } catch (Exception ex) {
                return NoContent();
            }
        }

        //Add Exam With Question
        [HttpPost]
        public async Task<IActionResult> AddExam([FromBody] AddExamDTO examDTO) {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await service.AddAsync(examDTO);
            return Created();
        }
        [HttpPut("id")]
        public async Task<IActionResult> Update(int examid, ExamUpdateDTO examDto) {
            if (examid <= 0) return BadRequest();
            try {
                await service.Update(examid, examDto);
                return NoContent();
            } catch (Exception ex) {
                return BadRequest();
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id) {
            try {
                if (id <= 0) return BadRequest();
                await service.Delete(id);
                return NoContent();
            } catch (Exception ex) {
                return BadRequest();
            }
        }
    }
}