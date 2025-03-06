using ASPNET_API.Domain.Entities;
using ASPNET_API.Domain.Interface.Repositories;

namespace ASPNET_API.Application.Services
{
    public class ConsultationRequestService
    {
        private readonly IConsultationRequestRepository _repository;

        public ConsultationRequestService(IConsultationRequestRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<ConsultationRequest>> GetAllConsultationRequestsAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<ConsultationRequest> GetConsultationRequestByIdAsync(int? id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task AddConsultationRequestAsync(ConsultationRequest request)
        {
            await _repository.AddAsync(request);
        }

        public async Task<bool> UpdateConsultationRequestAsync(int? id, ConsultationRequest request, int userId)
        {
            if (id != request.ConsultationRequestId)
                return false;

            request.ResolvedById = userId;
            await _repository.UpdateAsync(request);
            return true;
        }

        public async Task<bool> ChangeStatusAsync(int? id, string status, int userId)
        {
            var conre = await _repository.GetByIdAsync(id);
            if (conre == null) return false;

            conre.ResolvedById = userId;
            conre.IsResolved = status?.ToLower() == "resolved";

            if (!conre.IsResolved)
                conre.ResolvedById = null;

            await _repository.UpdateAsync(conre);
            return true;
        }

        public async Task<bool> DeleteConsultationRequestAsync(int? id)
        {
            var request = await _repository.GetByIdAsync(id);
            if (request == null)
                return false;

            await _repository.DeleteAsync(request);
            return true;
        }
    }
}
