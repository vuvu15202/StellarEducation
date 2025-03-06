using ASPNET_API.Domain.Entities;
using ASPNET_API.Domain.Interface.Repositories;
using ASPNET_API.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ASPNET_API.Infrastructure.Repositories
{
    public class ConsultationRequestRepository : IConsultationRequestRepository
    {
        private readonly DonationWebApp_v2Context _context;

        public ConsultationRequestRepository(DonationWebApp_v2Context context)
        {
            _context = context;
        }

        public async Task<List<ConsultationRequest>> GetAllAsync()
        {
            return await _context.ConsultationRequests
                .Include(u => u.ResolvedBy)
                .OrderBy(item => item.IsResolved)
                .ThenBy(item => item.CreatedAt)
                .ToListAsync();
        }

        public async Task<ConsultationRequest> GetByIdAsync(int? id)
        {
            return await _context.ConsultationRequests.FindAsync(id);
        }

        public async Task AddAsync(ConsultationRequest request)
        {
            _context.ConsultationRequests.Add(request);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ConsultationRequest request)
        {
            _context.Entry(request).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(ConsultationRequest request)
        {
            _context.ConsultationRequests.Remove(request);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(int? id)
        {
            return await _context.ConsultationRequests.AnyAsync(e => e.ConsultationRequestId == id);
        }
    }
}
