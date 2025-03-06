using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ASPNET_API.Domain.Entities;

namespace ASPNET_API.Domain.Interface.Repositories
{
    public interface IExamCandidateRepository
    {
        Task<IEnumerable<ExamCandidate>> GetAllAsync();
        Task<ExamCandidate?> GetByIdAsync(int id);
        Task<IEnumerable<ExamCandidate>> GetByCandidateIdAsync(int candidateId);
        Task<ExamCandidate> GetByIdAndCandidateIdAsync(int id,int candidateId);
        Task<IEnumerable<ExamCandidate>> GetByQuestionBankIdAsync(int questionBankId);
        Task<ExamCandidate> AddAsync(ExamCandidate examCandidate);
        Task<ExamCandidate> UpdateAsync(ExamCandidate examCandidate);
        Task DeleteAsync(int id);
        Task<bool> ExamCandidateExistsAsync(int id);

        Task<QuestionBank> GetQuestionBankByIdAsync(int id);
        Task<ExamCandidate> GetExamCandidateByQuestionBankIdAsync(int questionBankId, int candidateId);
        Task<ExamCandidate> AddExamCandidateAsync(ExamCandidate examCandidate);
        Task<List<ExamCandidate>> GetExamCandidatesByQuestionBankIdAsync(int questionBankId);

        Task<List<User>> GetAllUsersAsync();
        Task<List<ExamCandidate>> GetRegisteredExamCandidatesAsync(int questionBankId);
        Task AddUsersAsync(List<User> users);
        Task AddUserRolesAsync(List<UserRole> userRoles);
        Task AddExamCandidatesAsync(List<ExamCandidate> candidates);
    }
}
