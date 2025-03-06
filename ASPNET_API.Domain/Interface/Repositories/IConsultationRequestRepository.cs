using ASPNET_API.Domain.Entities;

namespace ASPNET_API.Domain.Interface.Repositories
{
    public interface IConsultationRequestRepository
    {
        Task<List<ConsultationRequest>> GetAllAsync();
        Task<ConsultationRequest> GetByIdAsync(int? id);
        Task AddAsync(ConsultationRequest request);
        Task UpdateAsync(ConsultationRequest request);
        Task DeleteAsync(ConsultationRequest request);
        Task<bool> ExistsAsync(int? id);
    }
}
