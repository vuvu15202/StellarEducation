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
    public class QuestionBankRepository : IQuestionBankRepository
    {
        private readonly DonationWebApp_v2Context _context;

        public QuestionBankRepository(DonationWebApp_v2Context context)
        {
            _context = context;
        }

        public async Task<IEnumerable<QuestionBank>> GetAllAsync()
        {
            return await _context.QuestionBanks
                .Include(q => q.Lecturer)
                .Where(q => !q.IsDelete)
                .ToListAsync();
        }

        public async Task<QuestionBank?> GetByIdAsync(int id)
        {
            return await _context.QuestionBanks
                .Include(q => q.Lecturer)
                .Include(q => q.ExamCandidates)
                .Include(q => q.Lessons)
                .FirstOrDefaultAsync(q => q.QuestionBankId == id && !q.IsDelete);
        }

        public async Task<IEnumerable<QuestionBank>> GetByLecturerIdAsync(int lecturerId)
        {
            return await _context.QuestionBanks
                .Where(q => q.LecturerId == lecturerId && !q.IsDelete)
                .ToListAsync();
        }

        public async Task AddAsync(QuestionBank questionBank)
        {
            await _context.QuestionBanks.AddAsync(questionBank);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(QuestionBank questionBank)
        {
            _context.QuestionBanks.Update(questionBank);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var questionBank = await GetByIdAsync(id);
            if (questionBank != null)
            {
                questionBank.IsDelete = true;
                _context.QuestionBanks.Update(questionBank);
                await _context.SaveChangesAsync();
            }
        }
    }
}
