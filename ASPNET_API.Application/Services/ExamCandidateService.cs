using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ASPNET_API.Application.DTOs;
using ASPNET_API.Domain.Entities;
using ASPNET_API.Domain.Interface.Repositories;
using ASPNET_API.Infrastructure.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using BCryptNet = BCrypt.Net.BCrypt;


namespace ASPNET_API.Application.Services
{
    public class ExamCandidateService
    {
        private readonly IExamCandidateRepository _repository;
        private readonly IMapper _mapper;


        public ExamCandidateService(IExamCandidateRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ExamCandidate>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<ExamCandidate?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<ExamCandidate>> GetByCandidateIdAsync(int candidateId)
        {
            return await _repository.GetByCandidateIdAsync(candidateId);
        }

        public async Task<ExamCandidate> GetByIdAndCandidateIdAsync(int examCandidateId, int candidateId)
        {
            return await _repository.GetByIdAndCandidateIdAsync(examCandidateId, candidateId);
        }

        public async Task<IEnumerable<ExamCandidate>> GetByQuestionBankIdAsync(int questionBankId)
        {
            return await _repository.GetByQuestionBankIdAsync(questionBankId);
        }

        public async Task<bool> ExamCandidateExistsAsync(int id)
        {
            return await _repository.ExamCandidateExistsAsync(id);
        }

        public async Task UpdateAsync(ExamCandidateDTO examCandidateDto)
        {
            var examCandidate = _mapper.Map<ExamCandidate>(examCandidateDto);
            await _repository.UpdateAsync(examCandidate);
        }

        public async Task<ExamCandidateDTO> AddAsync(ExamCandidateDTO examCandidateDto)
        {
            var examCandidate = _mapper.Map<ExamCandidate>(examCandidateDto);
            var createdExamCandidate = await _repository.AddAsync(examCandidate);
            return _mapper.Map<ExamCandidateDTO>(createdExamCandidate);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }


        public async Task<ExamCandidateDTO> GetOrCreateExamCandidateAsync(int questionBankId, int candidateId)
        {
            var questionBank = await _repository.GetQuestionBankByIdAsync(questionBankId);
            if (questionBank == null)
            {
                throw new Exception("Không tìm thấy đề thi.");
            }

            var examCandidate = await _repository.GetExamCandidateByQuestionBankIdAsync(questionBankId, candidateId);

            if (examCandidate == null)
            {
                examCandidate = new ExamCandidate
                {
                    QuestionBankId = questionBankId,
                    CandidateId = candidateId
                };

                examCandidate = await _repository.AddExamCandidateAsync(examCandidate);
            }

            return _mapper.Map<ExamCandidateDTO>(examCandidate);
        }


        public async Task<List<ExamCandidateDTO>> GetExamCandidatesByQuestionBankIdAsync(int questionBankId)
        {
            var examCandidates = await _repository.GetExamCandidatesByQuestionBankIdAsync(questionBankId);
            return _mapper.Map<List<ExamCandidateDTO>>(examCandidates);
        }


        public async Task<string> UploadCandidatesExcelAsync(IFormFile file, int questionBankId)
        {
            if (file == null || file.Length == 0)
            {
                throw new Exception("Upload your Excel file to generate QuestionBank!");
            }

            List<User> newUsers = new List<User>();
            List<UserRole> newUserRoles = new List<UserRole>();
            List<ExamCandidate> newExamCandidates = new List<ExamCandidate>();

            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                using (var package = new ExcelPackage(stream))
                {
                    var worksheet = package.Workbook.Worksheets["Candidates"];
                    if (worksheet == null)
                    {
                        throw new Exception("Excel file must contain a sheet named 'Candidates'.");
                    }

                    var rowCount = worksheet.Dimension.Rows;
                    List<User> emails = new List<User>();

                    for (int row = 2; row <= rowCount; row++)
                    {
                        string email = worksheet.Cells[row, 2].Value?.ToString()?.Trim().ToLower();
                        string firstName = worksheet.Cells[row, 3].Value?.ToString()?.Trim();
                        string lastName = worksheet.Cells[row, 4].Value?.ToString()?.Trim();

                        if (Regex.IsMatch(email, @"^(?:(?:[^<>()[\]\\.,;:\s@""]+(?:\.[^<>()[\]\\.,;:\s@""]+)*)|(?:"".+""))@(?:[a-zA-Z0-9-]+\.)+[a-zA-Z]{2,}$"))
                        {
                            emails.Add(new User { Email = email, FirstName = firstName, LastName = lastName });
                        }
                        else
                        {
                            throw new Exception($"Invalid data in sheet {worksheet.Name}, row {row}!");
                        }
                    }

                    var existingUsers = await _repository.GetAllUsersAsync();
                    var unregisteredUsers = emails.Where(e => !existingUsers.Any(u => u.Email.Equals(e.Email, StringComparison.OrdinalIgnoreCase))).ToList();

                    foreach (var item in unregisteredUsers)
                    {
                        var newUser = new User
                        {
                            UserName = item.Email,
                            Password = BCryptNet.HashPassword("12345678@"),
                            FirstName = item.FirstName ?? "UserFirstName",
                            LastName = item.LastName ?? "UserLastName",
                            Phone = "",
                            Address = "",
                            Email = item.Email,
                            EnrollDate = DateTime.Now,
                            Active = true
                        };

                        newUsers.Add(newUser);
                    }

                    await _repository.AddUsersAsync(newUsers);

                    foreach (var user in newUsers)
                    {
                        newUserRoles.Add(new UserRole { RoleId = 4, UserId = user.UserId });
                    }

                    await _repository.AddUserRolesAsync(newUserRoles);

                    var registeredCandidates = await _repository.GetRegisteredExamCandidatesAsync(questionBankId);
                    var unregisteredExamCandidates = emails
                        .Where(email => !registeredCandidates.Any(e => e.Candidate.Email.Equals(email.Email, StringComparison.OrdinalIgnoreCase)))
                        .ToList();

                    foreach (var user in unregisteredExamCandidates)
                    {
                        var dbUser = existingUsers.FirstOrDefault(u => u.Email.Equals(user.Email, StringComparison.OrdinalIgnoreCase)) ?? newUsers.First(u => u.Email == user.Email);

                        newExamCandidates.Add(new ExamCandidate
                        {
                            QuestionBankId = questionBankId,
                            CandidateId = dbUser.UserId
                        });
                    }

                    await _repository.AddExamCandidatesAsync(newExamCandidates);
                }
            }

            return "Successful!";
        }

    }
}

