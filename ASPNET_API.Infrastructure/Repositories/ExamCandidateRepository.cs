using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ASPNET_API.Domain.Entities;
using ASPNET_API.Domain.Interface.Repositories;
using ASPNET_API.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ASPNET_API.Infrastructure.Repositories
{
    public class ExamCandidateRepository : IExamCandidateRepository
    {
        private readonly DonationWebApp_v2Context _context;

        public ExamCandidateRepository(DonationWebApp_v2Context context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ExamCandidate>> GetAllAsync()
        {
            return await _context.ExamCandidates
                .Include(e => e.Candidate)
                .Include(e => e.QuestionBank)
                .ToListAsync();
        }

        public async Task<ExamCandidate?> GetByIdAsync(int id)
        {
            return await _context.ExamCandidates
                .Include(e => e.Candidate)
                .Include(e => e.QuestionBank)
                .FirstOrDefaultAsync(e => e.ExamCandidateId == id);
        }

        public async Task<IEnumerable<ExamCandidate>> GetByCandidateIdAsync(int candidateId)
        {
            return await _context.ExamCandidates
                .Where(e => e.CandidateId == candidateId)
                .Include(e => e.QuestionBank)
                .ToListAsync();
        }

        public async Task<ExamCandidate> GetByIdAndCandidateIdAsync(int id, int candidateId)
        {
            return await _context.ExamCandidates
                .Where(e => e.CandidateId == candidateId && e.ExamCandidateId == id)
                .Include(e => e.QuestionBank)
                .SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<ExamCandidate>> GetByQuestionBankIdAsync(int questionBankId)
        {
            return await _context.ExamCandidates
                .Where(e => e.QuestionBankId == questionBankId)
                .Include(e => e.Candidate)
                .ToListAsync();
        }

        public async Task<bool> ExamCandidateExistsAsync(int id)
        {
            return await _context.ExamCandidates.AnyAsync(e => e.ExamCandidateId == id);
        }

        public async Task<ExamCandidate> UpdateAsync(ExamCandidate examCandidate)
        {
            _context.Entry(examCandidate).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return examCandidate;

        }

        public async Task<ExamCandidate> CreateAsync(ExamCandidate examCandidate)
        {
            _context.ExamCandidates.Add(examCandidate);
            await _context.SaveChangesAsync();
            return examCandidate;
        }

        public async Task DeleteAsync(int id)
        {
            var examCandidate = await GetByIdAsync(id);
            if (examCandidate != null)
            {
                _context.ExamCandidates.Remove(examCandidate);
                await _context.SaveChangesAsync();
            }
        }


        public async Task<QuestionBank> GetQuestionBankByIdAsync(int id)
        {
            return await _context.QuestionBanks.FindAsync(id);
        }

        public async Task<ExamCandidate> GetExamCandidateByQuestionBankIdAsync(int questionBankId, int candidateId)
        {
            return await _context.ExamCandidates
                .Include(e => e.Candidate)
                .Include(e => e.QuestionBank)
                .SingleOrDefaultAsync(e => e.QuestionBankId == questionBankId && e.CandidateId == candidateId);
        }

        public async Task<ExamCandidate> AddExamCandidateAsync(ExamCandidate examCandidate)
        {
            _context.ExamCandidates.Add(examCandidate);
            await _context.SaveChangesAsync();
            return examCandidate;
        }

        public async Task<List<ExamCandidate>> GetExamCandidatesByQuestionBankIdAsync(int questionBankId)
        {
            return await _context.ExamCandidates
                .Include(e => e.Candidate)
                .Include(e => e.QuestionBank)
                .Where(e => e.QuestionBankId == questionBankId)
                .ToListAsync();
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<List<ExamCandidate>> GetRegisteredExamCandidatesAsync(int questionBankId)
        {
            return await _context.ExamCandidates
                .Include(e => e.Candidate)
                .Where(e => e.QuestionBankId == questionBankId)
                .ToListAsync();
        }

        public async Task AddUsersAsync(List<User> users)
        {
            await _context.Users.AddRangeAsync(users);
            await _context.SaveChangesAsync();
        }

        public async Task AddUserRolesAsync(List<UserRole> userRoles)
        {
            await _context.UserRoles.AddRangeAsync(userRoles);
            await _context.SaveChangesAsync();
        }

        public async Task AddExamCandidatesAsync(List<ExamCandidate> candidates)
        {
            await _context.ExamCandidates.AddRangeAsync(candidates);
            await _context.SaveChangesAsync();
        }
    }
}
