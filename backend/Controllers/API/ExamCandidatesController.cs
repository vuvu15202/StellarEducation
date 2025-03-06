using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ASPNET_API.Models.Entity;
using AutoMapper;
using ASPNET_API.Models.DTO;
using ASPNET_API.Models.Entity.TESTIELTS;
using OfficeOpenXml;
using System.Text.RegularExpressions;
using DocumentFormat.OpenXml.Spreadsheet;
using BCryptNet = BCrypt.Net.BCrypt;
using SQLitePCL;
using System.Data;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.AspNetCore.Routing.Matching;
namespace ASPNET_API.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamCandidatesController : ControllerBase
    {
        private readonly DonationWebApp_v2Context _context;
        private readonly IMapper _mapper;

        public ExamCandidatesController(DonationWebApp_v2Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/ExamCandidates
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExamCandidate>>> GetExamCandidates()
        {
          if (_context.ExamCandidates == null)
          {
              return NotFound();
          }
            return await _context.ExamCandidates.ToListAsync();
        }

        // GET: api/ExamCandidates
        [HttpGet("bystudentid")]
        public async Task<ActionResult> GetExamCandidatesOfAStudent()
        {
            if (_context.ExamCandidates == null)
            {
                return NotFound();
            }
            var sessionUser = (User)HttpContext.Items["User"];

            var examCandidates = await _context.ExamCandidates
                .Include(e => e.Candidate)
                .Include(e => e.QuestionBank)
                .Where(e => e.CandidateId == sessionUser.UserId).ToListAsync();
            var ec = _mapper.Map<List<ExamCandidateDTO>>(examCandidates);
            return Ok(ec);
        }

        // GET: api/ExamCandidates/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetExamCandidateStudent(int id)
        {
          if (_context.ExamCandidates == null)
          {
              return NotFound();
          }

            var sessionUser = (User)HttpContext.Items["User"];

            var examCandidate = await _context.ExamCandidates
                .Include(e => e.Candidate)
                .Include(e => e.QuestionBank)
                .SingleOrDefaultAsync(e => e.ExamCandidateId == id && e.CandidateId == sessionUser.UserId);

            if (examCandidate == null)
            {
                return NotFound("Test information could not be found, or you are not on the test list!");
            }

            var ec = _mapper.Map<ExamCandidateDTO>(examCandidate);

            return Ok(ec);
        }

		// GET: api/ExamCandidates/5
		[HttpGet("getByQuestionBankId/{id}")]
		public async Task<ActionResult> GetExamCandidateByQuestionbankId(int id)
		{
			if (_context.ExamCandidates == null)
			{
				return NotFound();
			}

			var sessionUser = (User)HttpContext.Items["User"]; if (sessionUser == null) return NotFound();

            QuestionBank questionBank = await _context.QuestionBanks.FindAsync(id);
            if(questionBank == null) return NotFound("Không tìm thấy đề thi.");

			var examCandidate = await _context.ExamCandidates
				.Include(e => e.Candidate)
				.Include(e => e.QuestionBank)
				.SingleOrDefaultAsync(e => e.QuestionBankId == id && e.CandidateId == sessionUser.UserId);

			if (examCandidate == null)
			{
				//return NotFound("Test information could not be found, or you are not on the test list!");
				examCandidate = new ExamCandidate()
                {
                    QuestionBankId = id,
                    CandidateId = sessionUser.UserId
                };
				_context.ExamCandidates.Add(examCandidate);
                await _context.SaveChangesAsync();
			}

			var ec = _mapper.Map<ExamCandidateDTO>(examCandidate);

			return Ok(ec);
		}

		// GET: api/ExamCandidates/5
		[HttpGet("candidates/{id}")]
        public async Task<ActionResult> GetExamCandidateStudentsByQuestionBank(int id)
        {
            if (_context.ExamCandidates == null)
            {
                return NotFound();
            }

            var examCandidate = await _context.ExamCandidates
                .Include(e => e.Candidate)
                .Include(e => e.QuestionBank)
                .Where(e => e.QuestionBankId == id).ToListAsync();

            //if (examCandidate == null)
            //{
            //    return NotFound("Test information could not be found!");
            //}

            var ec = _mapper.Map<List<ExamCandidateDTO>>(examCandidate);

            return Ok(ec);
        }

        // GET: api/ExamCandidates/5
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
			if (_context.QuestionBanks == null || _context.ExamCandidates == null)
			{
				return NotFound();
			}
			var sessionUser = (User)HttpContext.Items["User"];
			var examcandidate = await _context.ExamCandidates
                .Include(e => e.QuestionBank)
                .SingleOrDefaultAsync(e => e.CandidateId == sessionUser.UserId && e.ExamCandidateId == id);
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
            if (_context.QuestionBanks == null || _context.ExamCandidates == null)
            {
                return NotFound();
            }
            var sessionUser = (User)HttpContext.Items["User"];
            var examcandidate = await _context.ExamCandidates
                .Include(e => e.QuestionBank)
                .SingleOrDefaultAsync(e => e.CandidateId == sessionUser.UserId && e.ExamCandidateId == id);
            if (examcandidate == null)
            {
                return NotFound("Test information could not be found, or you are not on the test list!");
            }

            if (question.ToLower().Equals("reading"))
            {
                return Ok(new { QuestionBankId = examcandidate.QuestionBankId, 
                    readingJSON = examcandidate.QuestionBank.ReadingJSON,
                    answers = examcandidate.SubmitedReading
                });
            }
            else if (question.ToLower().Equals("listening"))
            {
                return Ok(new { QuestionBankId = examcandidate.QuestionBankId, 
                    listeningJSON = examcandidate.QuestionBank.ListeningJSON,
                    answers = examcandidate.SubmitedListening
                });
            }
            else
            {
                return Ok(new { QuestionBankId = examcandidate.QuestionBankId, 
                    writingJSON = examcandidate.QuestionBank.WritingJSON,
                    answers = examcandidate.SubmitedWriting
                });
            }
        }

        // PUT: api/ExamCandidates/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExamCandidate(int id, ExamCandidate examCandidate)
        {
            if (id != examCandidate.ExamCandidateId)
            {
                return BadRequest();
            }

            _context.Entry(examCandidate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExamCandidateExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ExamCandidates
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ExamCandidate>> PostExamCandidate(ExamCandidate examCandidate)
        {
          if (_context.ExamCandidates == null)
          {
              return Problem("Entity set 'DonationWebApp_v2Context.ExamCandidates'  is null.");
          }
            _context.ExamCandidates.Add(examCandidate);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetExamCandidate", new { id = examCandidate.ExamCandidateId }, examCandidate);
        }

        [HttpPost("UploadCandidatesExcel")]
        public async Task<ActionResult> UploadCandidatesExcel([FromForm]UpdateCandidate updateCandidate)
        {
            if (_context.ExamCandidates == null)
            {
                return Problem("Entity set 'DonationWebApp_v2Context.ExamCandidates'  is null.");
            }
            List<ExamCandidate> candidates= new List<ExamCandidate>();
            List<User> emails = new List<User>();

            if (updateCandidate.FileUploads != null && updateCandidate.FileUploads.Length != 0)
            {
                using (var stream = new MemoryStream())
                {
                    await updateCandidate.FileUploads.CopyToAsync(stream);

                    using (var package = new ExcelPackage(stream))
                    {
                        var workbook = package.Workbook;

                        var worksheetCandidates = workbook.Worksheets["Candidates"];
                        if (worksheetCandidates != null)
                        {
                            var rowcount = worksheetCandidates.Dimension.Rows;
                            for (int row = 2; row <= rowcount; row++)
                            {
                                try
                                {
                                    bool isEmail = Regex.IsMatch(worksheetCandidates.Cells[row, 2].Value?.ToString()?.Trim().ToLower(),
                                        @"^(?:(?:[^<>()[\]\\.,;:\s@""]+(?:\.[^<>()[\]\\.,;:\s@""]+)*)|(?:"".+""))@(?:[a-zA-Z0-9-]+\.)+[a-zA-Z]{2,}$");
                                    if (isEmail) emails.Add(new Models.Entity.User(){ Email = worksheetCandidates.Cells[row, 2].Value?.ToString()?.Trim().ToLower(),
                                                                    FirstName = worksheetCandidates.Cells[row, 3].Value?.ToString()?.Trim().ToLower(),
                                                                    LastName = worksheetCandidates.Cells[row, 4].Value?.ToString()?.Trim().ToLower()
                                    });
                                    else throw new Exception($"Vui lòng xem lại trang tính {worksheetCandidates.Name}, dòng {row}, nhập sai dữ liệu!");
                                }
                                catch (Exception ex)
                                {
                                    //throw new Exception($"Xem lại trang tính {worksheetParts.Name}, dòng {row}");
                                    return BadRequest(ex.Message);
                                }
                            }
                        }
                    }
                }
                var users = await _context.Users.ToListAsync();
                var unregisteredAccount = emails.Where(email => !users.Any(user => user.Email.Equals(email.Email, StringComparison.OrdinalIgnoreCase))).ToList();
                foreach (var item in unregisteredAccount)
                {
                    try
                    {
                        var newUser = new User();
                        newUser.UserName = item.Email;
                        newUser.Password = BCryptNet.HashPassword("12345678@");
                        newUser.FirstName = item.FirstName == null ? "UserFirstName": item.FirstName;
                        newUser.LastName = item.LastName == null ? "UserLastName" : item.LastName;
                        newUser.Phone = "";
                        newUser.Address = "";
                        newUser.Email = item.Email;
                        newUser.EnrollDate = DateTime.Now;
                        newUser.Active = true;

                        _context.Users.Add(newUser);
                        _context.SaveChanges();

                        var userRole = new UserRole();
                        userRole.RoleId = 4;
                        userRole.UserId = newUser.UserId;
                        _context.UserRoles.Add(userRole);
                        _context.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        return BadRequest(ex.InnerException);
                    }
                }

                var registeredExam = await _context.ExamCandidates.Include(e => e.Candidate).Where(e => e.QuestionBankId == updateCandidate.QuestionBankId).ToListAsync();
                var unregisteredExamEmail = emails.Where(email => !registeredExam.Any(e => e.Candidate.Email.ToLower().Equals(email.Email.ToLower()))).ToList();
                foreach (var item in unregisteredExamEmail)
                {
                    var newUser = await _context.Users.Where(u => u.Email.ToLower().Equals(item.Email.ToLower())).SingleOrDefaultAsync(); 
                    candidates.Add(new ExamCandidate()
                    {
                        QuestionBankId = updateCandidate.QuestionBankId,
                        CandidateId = newUser.UserId,

                    });
                }
                await _context.ExamCandidates.AddRangeAsync(candidates);
                await _context.SaveChangesAsync();
                return Ok("Successful!");

            }
            else
            {
                throw new Exception($"Upload your excel file to generate QuestionBank!");
            }

            //_context.ExamCandidates.AddRange(examCandidate);
            //await _context.SaveChangesAsync();

            //return CreatedAtAction("GetExamCandidate", new { id = examCandidate.ExamCandidateId }, examCandidate);
            return Ok();
        }

        // DELETE: api/ExamCandidates/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExamCandidate(int id)
        {
            if (_context.ExamCandidates == null)
            {
                return NotFound();
            }
            var examCandidate = await _context.ExamCandidates.FindAsync(id);
            if (examCandidate == null)
            {
                return NotFound();
            }

            examCandidate.IsDelete = true;
            _context.ExamCandidates.Update(examCandidate);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ExamCandidateExists(int id)
        {
            return (_context.ExamCandidates?.Any(e => e.ExamCandidateId == id)).GetValueOrDefault();
        }
    }
}
