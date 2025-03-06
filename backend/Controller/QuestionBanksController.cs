using ASPNET_API.Application.DTOs.IELTS;
using ASPNET_API.Application.DTOs;
using ASPNET_API.Application.Services;
using ASPNET_API.Domain.Entities;
using ASPNET_API.Domain.Entities.IELTS;
using ASPNET_API.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using ASPNET_API.Application.Services.Interfa;

namespace ASPNET_API.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionBanksController : ControllerBase
    {
        private readonly DonationWebApp_v2Context _context;
        private readonly IWebHostEnvironment _environment;
        private readonly IQuestionBankUtilService _questionBankService;
        private readonly IConfiguration _configuration;

        public QuestionBanksController(DonationWebApp_v2Context context, IWebHostEnvironment environment, IQuestionBankUtilService questionBankService, IConfiguration configuration)
        {
            _context = context;
            _environment = environment;
            _questionBankService = questionBankService;
            _configuration = configuration;
        }

        [HttpGet("public")]
        public async Task<ActionResult> GetPublicQuestionBank()
        {
            if (_context.QuestionBanks == null)
            {
                return NotFound();
            }
            var now = DateTime.Now;
            var questionBanks = await _context.QuestionBanks.Include(q => q.Lecturer).Include(q => q.Lecturer)
                .OrderByDescending(q => q.StartDate >= now) // Ngày >= DateTime.Now lên trước
                .ThenBy(q => Math.Abs(EF.Functions.DateDiffDay(now, q.StartDate))) // Ngày gần nhất với DateTime.Now
                .ThenByDescending(q => q.StartDate) // Phân giải thêm nếu trùng
                .Where(q => q.IsPrivate == false)
                .ToListAsync();
            var sto = questionBanks.Select(s => new
            {
                QuestionBankId = s.QuestionBankId,
                ExamCode = s.ExamCode,
                StartDate = s.StartDate,
                EndDate = s.EndDate,
                StartDateString = s.StartDate.ToString("dd/MM/yyyy, 'at' hh:mm tt"), //:ss   MMMM
                EndDateString = s.EndDate.ToString("dd/MM/yyyy, 'at' hh:mm tt"),
                Lecturer = s.Lecturer,
                IsClosed = s.IsClosed
            });
            return Ok(sto);
        }

        // GET: api/QuestionBanks
        [HttpGet]
        public async Task<ActionResult> GetQuestionBanks()
        {
            if (_context.QuestionBanks == null)
            {
                return NotFound();
            }
            var now = DateTime.Now;
            var questionBanks = await _context.QuestionBanks.Include(q => q.Lecturer).Include(q => q.Lecturer)
                .OrderByDescending(q => q.StartDate >= now) // Ngày >= DateTime.Now lên trước
                .ThenBy(q => Math.Abs(EF.Functions.DateDiffDay(now, q.StartDate))) // Ngày gần nhất với DateTime.Now
                .ThenByDescending(q => q.StartDate) // Phân giải thêm nếu trùng
                .ToListAsync();
            var sto = questionBanks.Select(s => new
            {
                QuestionBankId = s.QuestionBankId,
                ExamCode = s.ExamCode,
                StartDate = s.StartDate,
                EndDate = s.EndDate,
                StartDateString = s.StartDate.ToString("dd/MM/yyyy, 'at' hh:mm tt"), //:ss   MMMM
                EndDateString = s.EndDate.ToString("dd/MM/yyyy, 'at' hh:mm tt"),
                Lecturer = s.Lecturer,
                IsClosed = s.IsClosed,
                IsPrivate = s.IsPrivate
            });
            return Ok(sto);
        }

        // GET: api/QuestionBanks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<QuestionBank>> GetQuestionBank(int id)
        {
            if (_context.QuestionBanks == null)
            {
                return NotFound();
            }
            var questionBank = await _context.QuestionBanks.Include(q => q.Lecturer).SingleOrDefaultAsync(q => q.QuestionBankId == id);
            var sto = new QuestionBank()
            {
                QuestionBankId = questionBank.QuestionBankId,
                ExamCode = questionBank.ExamCode,
                StartDate = questionBank.StartDate,
                EndDate = questionBank.EndDate,
                Lecturer = questionBank.Lecturer,
                IsClosed = questionBank.IsClosed
            };
            //var abc = await _context.QuestionBanks.FindAsync(1);
            //abc.Listening = JsonSerializer.Serialize(data.Listening);
            //_context.QuestionBanks.Update(abc);
            //await _context.SaveChangesAsync();
            if (questionBank == null)
            {
                return NotFound();
            }

            return questionBank;
        }

        [HttpGet("question/{id}/{question}")]
        public async Task<ActionResult> GetQuestion(int id, string question)
        {
            if (_context.QuestionBanks == null)
            {
                return NotFound();
            }
            var sessionUser = (User)HttpContext.Items["User"];
            var questionBank = await _context.QuestionBanks
                .Include(q => q.Lecturer)
                .SingleOrDefaultAsync(q => q.QuestionBankId == id);

            if (questionBank == null)
            {
                return NotFound();
            }
            var examcandidate = await _context.ExamCandidates.SingleOrDefaultAsync(e => e.CandidateId == sessionUser.UserId && e.QuestionBankId == questionBank.QuestionBankId);
            if (examcandidate == null)
            {
                return NotFound("Không tìm thấy thông tin  đề thi, hoặc bạn không có trong danh sách thi!");
            }

            if (question.ToLower().Equals("reading"))
            {
                if (!examcandidate.SubmitedReading.Equals("[]")) return BadRequest("Bạn đã làm bài thi này!");
                return Ok(new { readingJSON = questionBank.ReadingJSON });
            }
            else if (question.ToLower().Equals("listening"))
            {
                if (!examcandidate.SubmitedListening.Equals("[]")) return BadRequest("Bạn đã làm bài thi này!");
                return Ok(new { listeningJSON = questionBank.ListeningJSON });
            }
            else
            {
                if (!examcandidate.SubmitedWriting.Equals("[]")) return BadRequest("Bạn đã làm bài thi này!");
                return Ok(new { writingJSON = questionBank.WritingJSON });
            }
        }

        [HttpGet("editquestion/{id}/{question}")]
        public async Task<ActionResult> GetEditQuestion(int id, string question)
        {
            if (_context.QuestionBanks == null)
            {
                return NotFound();
            }
            var questionBank = await _context.QuestionBanks
                .Include(q => q.Lecturer)
                .SingleOrDefaultAsync(q => q.QuestionBankId == id);

            if (questionBank == null)
            {
                return NotFound();
            }

            if (question.ToLower().Equals("reading"))
            {
                return Ok(new { readingJSON = questionBank.ReadingJSON });
            }
            else if (question.ToLower().Equals("listening"))
            {
                return Ok(new { listeningJSON = questionBank.ListeningJSON });
            }
            else
            {
                return Ok(new { writingJSON = questionBank.WritingJSON });
            }
        }

        [HttpGet("getAnswersWriting/{examCandidateId}")]
        public async Task<ActionResult> GetAnswersWriting(int examCandidateId)
        {
            if (_context.QuestionBanks == null || _context.ExamCandidates == null)
            {
                return NotFound();
            }
            var examCandidate = await _context.ExamCandidates
                .SingleOrDefaultAsync(q => q.ExamCandidateId == examCandidateId);
            if (examCandidate == null) return NotFound("Không tìm thấy bài thi!");
            return Ok(new { answers = examCandidate.SubmitedWriting });

        }

        [HttpPost("gradeAnswersWriting/{examCandidateId}")]
        public async Task<ActionResult> gradeAnswersWriting(int examCandidateId, [FromBody] List<double> grades)
        {
            if (_context.QuestionBanks == null || _context.ExamCandidates == null)
            {
                return NotFound();
            }
            var bandscoreWriting = CalculateWritingBand(grades);
            var ec = await _context.ExamCandidates.FindAsync(examCandidateId);
            if (ec == null) return NotFound("Không tìm thấy bài thi!");
            ec.BandScoreWriting = bandscoreWriting;
            ec.Overall = await _questionBankService.CalculateOverall((double)ec.BandScoreReading, (double)ec.BandScoreListening, (double)bandscoreWriting, 0);
            _context.ExamCandidates.Update(ec);
            await _context.SaveChangesAsync();

            return Ok("Chấm điểm thành công!");

        }

        private double CalculateWritingBand(List<double> grades)
        {
            // Tính điểm trung bình của các tiêu chí
            double totalScore = grades.Average();//sum() /5

            // Làm tròn tới 0.5 gần nhất
            double bandScore = Math.Round(totalScore * 2) / 2;

            return bandScore;
        }

        // GET: api/QuestionBanks/5
        [HttpGet("examcode/{examCode}")]
        public async Task<ActionResult<QuestionBank>> GetQuestionBankByExamCode(string examCode)
        {
            if (_context.QuestionBanks == null)
            {
                return NotFound();
            }
            var questionBank = await _context.QuestionBanks.Include(q => q.Lecturer).SingleOrDefaultAsync(q => q.ExamCode == examCode);

            if (questionBank == null)
            {
                return NotFound();
            }

            return questionBank;
        }

        // PUT: api/QuestionBanks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuestionBank(int id, QuestionBank questionBank)
        {
            if (id != questionBank.QuestionBankId)
            {
                return BadRequest("ID không khớp");
            }

            // Lấy thực thể từ cơ sở dữ liệu
            var existingEntity = await _context.QuestionBanks.FindAsync(id);
            if (existingEntity == null)
            {
                return NotFound();
            }

            questionBank.LecturerId = existingEntity.LecturerId;
            questionBank.Reading = existingEntity.Reading;
            questionBank.Listening = existingEntity.Listening;
            questionBank.Writing = existingEntity.Writing;
            // Gán giá trị mới cho thực thể đã lấy từ DbContext
            _context.Entry(existingEntity).CurrentValues.SetValues(questionBank);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuestionBankExists(id))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }


        // POST: api/QuestionBanks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<QuestionBank>> PostQuestionBank(QuestionBank questionBank)
        {
            if (_context.QuestionBanks == null)
            {
                return Problem("Entity set 'DonationWebApp_v2Context.QuestionBanks'  is null.");
            }
            var sessionUser = (User)HttpContext.Items["User"];
            questionBank.LecturerId = sessionUser.UserId;

            var checkExamCode = _context.QuestionBanks.FirstOrDefault(q => q.ExamCode.ToLower().Equals(questionBank.ExamCode.ToLower()));
            if (checkExamCode != null) return BadRequest("Exam Code đã tồn tại!");

            _context.QuestionBanks.Add(questionBank);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetQuestionBank", new { id = questionBank.QuestionBankId }, questionBank);
        }

        // DELETE: api/QuestionBanks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuestionBank(int id)
        {
            if (_context.QuestionBanks == null)
            {
                return NotFound();
            }
            var questionBank = await _context.QuestionBanks.FindAsync(id);
            if (questionBank == null)
            {
                return NotFound();
            }

            questionBank.IsDelete = true;
            _context.QuestionBanks.Update(questionBank);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool QuestionBankExists(int id)
        {
            return (_context.QuestionBanks?.Any(e => e.QuestionBankId == id)).GetValueOrDefault();
        }

        [HttpPost("UploadPicture")]
        public async Task<ActionResult> UploadPicture([FromForm] UpdatePicture updatePictureModel)
        {
            var checkQB = await _context.QuestionBanks.FindAsync(updatePictureModel.QuestionBankId);
            if (checkQB == null)
            {
                return NotFound("Not found Question Bank");
            }
            string fileName = "";
            string ext = "";
            if (updatePictureModel.FileUploads != null)
            {
                var uploads = Path.Combine(_environment.WebRootPath, "uploads", "ielts");

                if (!Directory.Exists(uploads))
                    Directory.CreateDirectory(uploads);

                var filePath = Path.Combine(uploads, updatePictureModel.FileUploads.FileName);
                ext = Path.GetExtension(updatePictureModel.FileUploads.FileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await updatePictureModel.FileUploads.CopyToAsync(stream);
                    fileName = updatePictureModel.FileUploads.FileName;
                }
            }

            try
            {
                if (updatePictureModel.ForQuestion.ToLower().Equals("reading"))
                {
                    _questionBankService.UploadFileReading(updatePictureModel, fileName, ext);
                }
                else if (updatePictureModel.ForQuestion.ToLower().Equals("listening"))
                {
                    _questionBankService.UploadFileListening(updatePictureModel, fileName, ext);
                }
                else if (updatePictureModel.ForQuestion.ToLower().Equals("writing"))
                {
                    _questionBankService.UploadFileWriting(updatePictureModel, fileName, ext);
                }
                else
                {
                    return BadRequest("Unexpected error!");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(fileName);
        }

        [HttpPost("UploadQuestionExcel")]
        public async Task<ActionResult> UploadQuestion([FromForm] UpdateQuestion updateQuestionModel)
        {
            var questionBank = await _context.QuestionBanks.FindAsync(updateQuestionModel.QuestionBankId);
            if (questionBank == null)
            {
                return NotFound("Not Found Question Bank id = " + updateQuestionModel.QuestionBankId);
            }

            try
            {
                List<Part> parts = await _questionBankService.RetrieveExcelDataAsync(updateQuestionModel.FileUploads);
                if (updateQuestionModel.FileAudio != null) await _questionBankService.UploadFileAudio(updateQuestionModel.FileAudio, updateQuestionModel.QuestionBankId);
                QuestionBank qs = await _context.QuestionBanks.FindAsync(updateQuestionModel.QuestionBankId);

                if (updateQuestionModel.ForQuestion.ToLower().Equals("reading"))
                {
                    var time = updateQuestionModel.Time != null ? (Int32.Parse(updateQuestionModel.Time) * 60).ToString() : qs.ReadingJSON.Time;
                    var partss = parts != null ? parts : qs.ReadingJSON.Parts;
                    questionBank.Reading = JsonSerializer.Serialize(new Reading() { Time = time, Parts = partss });
                    _context.Update(questionBank);
                    await _context.SaveChangesAsync();
                }
                else if (updateQuestionModel.ForQuestion.ToLower().Equals("listening"))
                {
                    var time = updateQuestionModel.Time != null ? (Int32.Parse(updateQuestionModel.Time) * 60).ToString() : qs.ListeningJSON.Time;
                    var partss = parts != null ? parts : qs.ListeningJSON.Parts;
                    questionBank.Listening = JsonSerializer.Serialize(new Listening() { Time = time, ListeningFileURL = qs.ListeningJSON.ListeningFileURL, Parts = partss });
                    _context.Update(questionBank);
                    await _context.SaveChangesAsync();
                }
                else if (updateQuestionModel.ForQuestion.ToLower().Equals("writing"))
                {
                    var time = updateQuestionModel.Time != null ? (Int32.Parse(updateQuestionModel.Time) * 60).ToString() : qs.WritingJSON.Time;
                    var partss = parts != null ? parts : qs.WritingJSON.Parts;
                    questionBank.Writing = JsonSerializer.Serialize(new Writing() { Time = time, Parts = partss });
                    _context.Update(questionBank);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    return BadRequest("Unexpected Error, please try again!");
                }
                return Ok(parts);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpPost("GradeAdmin")]
        public async Task<ActionResult> GradeForAdmin(SubmitedAnswersDTO submitedAnswers)
        {
            var questionBank = await _context.QuestionBanks.FindAsync(submitedAnswers.QuestionBankId);
            if (questionBank == null) return NotFound("Your QuestionBank is notfound!");

            var hashSetB = new HashSet<(string, string)>(submitedAnswers.Answers.Select(b => (b.QuestionNo, b.SubmitedAnswer)));
            if (submitedAnswers.ForQuestion.ToLower().Equals("reading"))
            {
                var allQuestions = questionBank.ReadingJSON?.Parts.SelectMany(p => p.Groups)
                             .SelectMany(g => g.Questions)
                             //.Union(
                             //    data.Listening?.Parts.SelectMany(p => p.Groups)
                             //    .SelectMany(g => g.Questions) ?? Enumerable.Empty<Question>()
                             //)
                             .Select(q => new { q.QuestionNo, q.CorrectAnswer })
                             .ToList();
                int intersectCount = allQuestions.Count(a => hashSetB.Contains((a.QuestionNo, a.CorrectAnswer)));
                return Ok(intersectCount);
            }
            else if (submitedAnswers.ForQuestion.ToLower().Equals("listening"))
            {
                var allQuestions = questionBank.ListeningJSON?.Parts.SelectMany(p => p.Groups)
                             .SelectMany(g => g.Questions)
                             .Select(q => new { q.QuestionNo, q.CorrectAnswer })
                             .ToList();
                int intersectCount = allQuestions.Count(a => hashSetB.Contains((a.QuestionNo, a.CorrectAnswer)));
                return Ok(intersectCount);
            }
            else if (submitedAnswers.ForQuestion.ToLower().Equals("writing"))
            {
                return Ok("Your answers have been saved. Please wait for lecturer to grade you.");
            }

            return BadRequest("Unexpected Error, Please try again!");
        }



        [HttpPost("Grade")]
        public async Task<ActionResult> Grade(SubmitedAnswersDTO submitedAnswers)
        {
            var questionBank = await _context.QuestionBanks.FindAsync(submitedAnswers.QuestionBankId);
            if (questionBank == null) return NotFound("Your QuestionBank is notfound!");

            var sessionUser = (User)HttpContext.Items["User"];
            var examcandidate = await _context.ExamCandidates.SingleOrDefaultAsync(e => e.CandidateId == sessionUser.UserId && e.QuestionBankId == questionBank.QuestionBankId);
            if (examcandidate == null)
            {
                return NotFound("Test information could not be found, or you are not on the test list!");
            }

            try
            {
                var hashSetB = new HashSet<(string, string)>(submitedAnswers.Answers.Select(b => (b.QuestionNo, b.SubmitedAnswer)));
                int intersectCount = 0;
                if (submitedAnswers.ForQuestion.ToLower().Equals("reading"))
                {
                    var allQuestions = questionBank.ReadingJSON?.Parts.SelectMany(p => p.Groups)
                                 .SelectMany(g => g.Questions)
                                 .Select(q => new { q.QuestionNo, q.CorrectAnswer })
                                 .ToList();
                    intersectCount = allQuestions.Count(a => hashSetB.Contains((a.QuestionNo, a.CorrectAnswer)));
                    examcandidate.SubmitedReading = JsonSerializer.Serialize(submitedAnswers.Answers);
                    examcandidate.BandScoreReading = await _questionBankService.CalculateBandScore(intersectCount, "reading");
                    examcandidate.CorrectAnswersReading = intersectCount;
                }
                else if (submitedAnswers.ForQuestion.ToLower().Equals("listening"))
                {
                    var allQuestions = questionBank.ListeningJSON?.Parts.SelectMany(p => p.Groups)
                                 .SelectMany(g => g.Questions)
                                 .Select(q => new { q.QuestionNo, q.CorrectAnswer })
                                 .ToList();
                    intersectCount = allQuestions.Count(a => hashSetB.Contains((a.QuestionNo, a.CorrectAnswer)));
                    examcandidate.SubmitedListening = JsonSerializer.Serialize(submitedAnswers.Answers);
                    examcandidate.BandScoreListening = await _questionBankService.CalculateBandScore(intersectCount, "listening");
                    examcandidate.CorrectAnswersListening = intersectCount;
                }
                else if (submitedAnswers.ForQuestion.ToLower().Equals("writing"))
                {
                    examcandidate.SubmitedWriting = JsonSerializer.Serialize(submitedAnswers.Answers);
                    examcandidate.BandScoreWriting = await _questionBankService.CalculateBandScore(intersectCount, "writing");
                }
                else
                {
                    return BadRequest("Unexpected Error, Please try again!");
                }
                examcandidate.Overall = await _questionBankService.CalculateOverall((double)examcandidate.BandScoreReading, (double)examcandidate.BandScoreListening, 0, 0);

                //check complete
                var isDone = true;
                if (questionBank.ReadingJSON.Parts != null && examcandidate.SubmitedReading.Equals("[]")) isDone = false;
                if (questionBank.ListeningJSON.Parts != null && examcandidate.SubmitedListening.Equals("[]")) isDone = false;
                if (questionBank.WritingJSON.Parts != null && examcandidate.SubmitedWriting.Equals("[]")) isDone = false;
                examcandidate.IsComplete = isDone;
                //



                _context.ExamCandidates.Update(examcandidate);
                await _context.SaveChangesAsync();

                // 
                if (!string.IsNullOrEmpty(examcandidate.TypeExam) && examcandidate.TypeExam.Equals("finalquiz"))
                {
                    var courseEnroll = await _context.CourseEnrolls.Where(c => c.Quiz.Contains(examcandidate.ExamCandidateId.ToString())).SingleOrDefaultAsync();
                    if (courseEnroll != null)
                    {
                        courseEnroll.CourseStatus = 1;
                        _context.CourseEnrolls.Update(courseEnroll);
                        await _context.SaveChangesAsync();
                    }
                }

                return Ok("Your questions have been saved!.");
            }
            catch (Exception ex)
            {
                return BadRequest("Unexpected Error, Please try again!");
            }
            return Ok();
        }

        [HttpGet("template/download/{forQuestion}")]
        public IActionResult DownloadFile(string forQuestion)
        {
            var filePath = "";
            var fileName = "sample.pdf";

            if (forQuestion.ToLower().Equals("reading"))
            {
                filePath = Path.Combine(_environment.WebRootPath, "uploads", "templateexcel", "Question_Bank_Reading.xlsx");
                fileName = "Question_Bank_Reading.xlsx";
            }
            else if (forQuestion.ToLower().Equals("listening"))
            {
                filePath = Path.Combine(_environment.WebRootPath, "uploads", "templateexcel", "Question_Bank_Listening.xlsx");
                fileName = "Question_Bank_Listening.xlsx";
            }
            else if (forQuestion.ToLower().Equals("writing"))
            {
                filePath = Path.Combine(_environment.WebRootPath, "uploads", "templateexcel", "Question_Bank_Writing.xlsx");
                fileName = "Question_Bank_Writing.xlsx";
            }
            else if (forQuestion.ToLower().Equals("candidates"))
            {
                filePath = Path.Combine(_environment.WebRootPath, "uploads", "templateexcel", "Template_Candidates_List.xlsx");
                fileName = "Template_Candidates_List.xlsx";
            }

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound(new { message = "File not found" });
            }

            // Đọc tệp và trả về
            var fileBytes = System.IO.File.ReadAllBytes(filePath);
            return File(fileBytes, "application/pdf", fileName);
        }

    }
}
