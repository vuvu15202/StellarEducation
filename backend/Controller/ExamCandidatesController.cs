using ASPNET_API.Application.DTOs;
using ASPNET_API.Application.Services;
using ASPNET_API.Domain.Entities;
using ASPNET_API.Infrastructure.Data;
using AutoMapper;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace ASPNET_API.Controller
{
    public class ExamCandidatesController : ControllerBase
    {
        private readonly DonationWebApp_v2Context _context;
        private readonly IMapper _mapper;
        private readonly ExamCandidateService _examCandidateService;

        public ExamCandidatesController(DonationWebApp_v2Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        // GET: api/ExamCandidates
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExamCandidate>>> GetExamCandidates()
        {

            return Ok(await _examCandidateService.GetAllAsync());
        }




        // GET: api/ExamCandidates
        [HttpGet("bystudentid")]
        public async Task<ActionResult> GetExamCandidatesOfAStudent()
        {
            var sessionUser = (User)HttpContext.Items["User"];
            return Ok(_examCandidateService.GetByCandidateIdAsync(sessionUser.UserId));
        }



        // GET: api/ExamCandidates/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetExamCandidateStudent(int id)
        {
            var sessionUser = (User)HttpContext.Items["User"];

            var examCandidate = _examCandidateService.GetByIdAndCandidateIdAsync(id, sessionUser.UserId);

            if (examCandidate == null)
            {
                return NotFound("Test information could not be found, or you are not on the test list!");
            } var ec = _mapper.Map<ExamCandidateDTO>(examCandidate);

            return Ok(ec);
        }



        [HttpGet("getByQuestionBankId/{id}")]
        public async Task<IActionResult> GetExamCandidateByQuestionbankId(int id)
        {
            try
            {
                var sessionUser = (User)HttpContext.Items["User"];
                if (sessionUser == null) return NotFound();

                var examCandidateDto = await _examCandidateService.GetOrCreateExamCandidateAsync(id, sessionUser.UserId);
                return Ok(examCandidateDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("candidates/{id}")]
        public async Task<IActionResult> GetExamCandidateStudentsByQuestionBank(int id)
        {
            try
            {
                var examCandidates = await _examCandidateService.GetExamCandidatesByQuestionBankIdAsync(id);
                return Ok(examCandidates);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpGet("exportcadidates/{id}")]
        public IActionResult ExportExamCandidateByQuestionBank(int id)
        {
            if (_context.ExamCandidates == null)
            {
                return NotFound();
            }

            var examCandidates = _context.ExamCandidates
                .Include(e => e.Candidate)
                .Include(e => e.QuestionBank)
                .Where(e => e.QuestionBankId == id).ToList();
            return GenerateExcel($"reportResultQuestionBank_{id}.xlsx", examCandidates);
        }



        private FileResult GenerateExcel(string fileName, IEnumerable<ExamCandidate> examcandidates)
        {
            DataTable dataTable = new DataTable("report");
            dataTable.Columns.AddRange(new DataColumn[]
            {
                new DataColumn("No"),
                new DataColumn("Email"),
                new DataColumn("FirstName"),
                new DataColumn("LastName"),
                new DataColumn("Correct Answers Reading"),
                new DataColumn("BandScore Reading"),
                new DataColumn("Correct Answers Listening"),
                new DataColumn("BandScore Listening"),
                new DataColumn("BandScore Writing")
            });

            foreach (var (examcandidate, index) in examcandidates.Select((value, i) => (value, i)))
            {
                dataTable.Rows.Add(
                    index,
                    examcandidate.Candidate!.Email,
                    examcandidate.Candidate.FirstName,
                    examcandidate.Candidate.LastName,
                    examcandidate.CorrectAnswersReading,
                    examcandidate.BandScoreReading,
                    examcandidate.CorrectAnswersListening,
                    examcandidate.BandScoreListening,
                    examcandidate.BandScoreWriting);
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                //wb.Worksheets.Add(dataTable);
                wb.Worksheets.Add(dataTable, "report");

                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);

                    return File(stream.ToArray(),
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        fileName);
                }
            }
        }


        [HttpGet("{id}/{question}")]
        public async Task<ActionResult> GetQuestion(int id, string question)
        {
            var sessionUser = (User)HttpContext.Items["User"];
            var examcandidate = await  _examCandidateService.GetByIdAndCandidateIdAsync(id, sessionUser.UserId);
            if (examcandidate == null)
            {
                return NotFound("Test information could not be found, or you are not on the test list!");
            }

            if (examcandidate.QuestionBank.IsClosed || !(examcandidate.QuestionBank.StartDate <= DateTime.Now && DateTime.Now <= examcandidate.QuestionBank.EndDate))
                return BadRequest("Rất tiếc, hiện tại đề thi đang đóng!");

            if (question.ToLower().Equals("reading"))
            {
                return Ok(new { QuestionBankId = examcandidate.QuestionBankId, readingJSON = examcandidate.QuestionBank.ReadingJSON });
            }
            else if (question.ToLower().Equals("listening"))
            {
                return Ok(new { QuestionBankId = examcandidate.QuestionBankId, listeningJSON = examcandidate.QuestionBank.ListeningJSON });
            }
            else
            {
                return Ok(new { QuestionBankId = examcandidate.QuestionBankId, writingJSON = examcandidate.QuestionBank.WritingJSON });
            }
        }



        [HttpGet("review/{id}/{question}")]
        public async Task<ActionResult> GetQuestionReview(int id, string question)
        {
            var sessionUser = (User)HttpContext.Items["User"];
            var examcandidate = await _examCandidateService.GetByIdAndCandidateIdAsync(id, sessionUser.UserId);

            if (examcandidate == null) return NotFound("Test information could not be found, or you are not on the test list!");

            if (question.ToLower().Equals("reading"))
            {
                return Ok(new
                {
                    QuestionBankId = examcandidate.QuestionBankId,
                    readingJSON = examcandidate.QuestionBank.ReadingJSON,
                    answers = examcandidate.SubmitedReading
                });
            }
            else if (question.ToLower().Equals("listening"))
            {
                return Ok(new
                {
                    QuestionBankId = examcandidate.QuestionBankId,
                    listeningJSON = examcandidate.QuestionBank.ListeningJSON,
                    answers = examcandidate.SubmitedListening
                });
            }
            else
            {
                return Ok(new
                {
                    QuestionBankId = examcandidate.QuestionBankId,
                    writingJSON = examcandidate.QuestionBank.WritingJSON,
                    answers = examcandidate.SubmitedWriting
                });
            }
        }



        [HttpPut("{id}")]
        public async Task<IActionResult> PutExamCandidate(int id, [FromBody] ExamCandidateDTO examCandidateDto)
        {
            if (id != examCandidateDto.ExamCandidateId) return BadRequest("ID mismatch");

            try
            {
                bool exists = await _examCandidateService.ExamCandidateExistsAsync(id);
                if (!exists)
                {
                    return NotFound("ExamCandidate not found.");
                }

                await _examCandidateService.UpdateAsync(examCandidateDto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error updating ExamCandidate: {ex.Message}");
            }
        }



        [HttpPost]
        public async Task<IActionResult> PostExamCandidate([FromBody] ExamCandidateDTO examCandidateDto)
        {
            try
            {
                var createdExamCandidate = await _examCandidateService.AddAsync(examCandidateDto);
                return Ok(createdExamCandidate);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error creating ExamCandidate: {ex.Message}");
            }
        }


        // DELETE: api/ExamCandidates/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExamCandidate(int id)
        {
            if (_context.ExamCandidates == null) return NotFound();

            var examCandidate = await _examCandidateService.GetByCandidateIdAsync(id);
            if (examCandidate == null) return NotFound();

            await _examCandidateService.DeleteAsync(id);

            return NoContent();
        }



        [HttpPost("UploadCandidatesExcel")]
        public async Task<IActionResult> UploadCandidatesExcel([FromForm] IFormFile file, [FromForm] int questionBankId)
        {
            try
            {
                string result = await _examCandidateService.UploadCandidatesExcelAsync(file, questionBankId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
