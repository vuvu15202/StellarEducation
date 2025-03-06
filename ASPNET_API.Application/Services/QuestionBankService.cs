using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ASPNET_API.Domain.Entities;
using ASPNET_API.Domain.Interface.Repositories;

namespace ASPNET_API.Application.Services
{
    public class QuestionBankService
    {
        private readonly IQuestionBankRepository _repository;

        public QuestionBankService(IQuestionBankRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<QuestionBank>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<QuestionBank?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<QuestionBank>> GetByLecturerIdAsync(int lecturerId)
        {
            return await _repository.GetByLecturerIdAsync(lecturerId);
        }

        public async Task AddAsync(QuestionBank questionBank)
        {
            await _repository.AddAsync(questionBank);
        }

        public async Task UpdateAsync(QuestionBank questionBank)
        {
            await _repository.UpdateAsync(questionBank);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
