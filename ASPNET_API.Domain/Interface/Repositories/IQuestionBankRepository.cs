using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ASPNET_API.Domain.Entities;

namespace ASPNET_API.Domain.Interface.Repositories
{
    public interface IQuestionBankRepository
    {
        Task<IEnumerable<QuestionBank>> GetAllAsync();
        Task<QuestionBank?> GetByIdAsync(int id);
        Task<IEnumerable<QuestionBank>> GetByLecturerIdAsync(int lecturerId);
        Task AddAsync(QuestionBank questionBank);
        Task UpdateAsync(QuestionBank questionBank);
        Task DeleteAsync(int id);
    }
}
