using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ASPNET_API.Models.Entity;
using ASPNET_API.Authorization;
using ASPNET_API.Models;
using AutoMapper;
using ASPNET_API.Models.DTO;

namespace ASPNET_API.Controllers.API
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultationRequestsController : ControllerBase
    {
        private readonly DonationWebApp_v2Context _context;
		private readonly IMapper _mapper;

		public ConsultationRequestsController(DonationWebApp_v2Context context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}



		// GET: api/ConsultationRequests
		[Authorize(RoleEnum.Admin, RoleEnum.Staff)]
		[HttpGet]
        public async Task<ActionResult> GetConsultationRequests()
        {
          if (_context.ConsultationRequests == null)
          {
              return NotFound();
          }
            var consultationRequests = await _context.ConsultationRequests
                .Include(u => u.ResolvedBy)
                .OrderBy(item => item.IsResolved) 
                .ThenBy(item => item.CreatedAt).ToListAsync();
			return Ok(_mapper.Map<List<ConsultationRequestDTO>>(consultationRequests));
        }

		// GET: api/ConsultationRequests/5
		[Authorize(RoleEnum.Admin, RoleEnum.Staff)]
		[HttpGet("{id}")]
        public async Task<ActionResult<ConsultationRequest>> GetConsultationRequest(int? id)
        {
          if (_context.ConsultationRequests == null)
          {
              return NotFound();
          }
            var consultationRequest = await _context.ConsultationRequests.FindAsync(id);

            if (consultationRequest == null)
            {
                return NotFound();
            }

            return consultationRequest;
        }

        // PUT: api/ConsultationRequests/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(RoleEnum.Admin, RoleEnum.Staff)]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutConsultationRequest(int? id, ConsultationRequest consultationRequest)
        {
            if (id != consultationRequest.ConsultationRequestId)
            {
                return BadRequest();
            }

            var user = HttpContext.Items["User"] as User;
            consultationRequest.ResolvedById = user.UserId;

            _context.Entry(consultationRequest).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ConsultationRequestExists(id))
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

        [Authorize(RoleEnum.Admin, RoleEnum.Staff)]
        [HttpPut("changetatus/{id}/{status}")]
        public async Task<IActionResult> ChangeStatus(int? id, string? status)
        {
            var conre = await _context.ConsultationRequests.FindAsync(id);
            if (conre == null) return NotFound("Không tìm thấy yêu cầu.");

            var user = HttpContext.Items["User"] as User;
            conre.ResolvedById = user.UserId;

            if(status != null && status.ToLower().Equals("resolved"))
            {
                conre.IsResolved = true;
            }else
            {
                conre.IsResolved = false;
                conre.ResolvedById = null;
            }

            _context.Entry(conre).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ConsultationRequestExists(id))
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

        // POST: api/ConsultationRequests
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<ConsultationRequest>> PostConsultationRequest(ConsultationRequest consultationRequest)
        {
          if (_context.ConsultationRequests == null)
          {
              return Problem("Entity set 'DonationWebApp_v2Context.ConsultationRequests'  is null.");
          }
            _context.ConsultationRequests.Add(consultationRequest);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetConsultationRequest", new { id = consultationRequest.ConsultationRequestId }, consultationRequest);
        }

        // DELETE: api/ConsultationRequests/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConsultationRequest(int? id)
        {
            if (_context.ConsultationRequests == null)
            {
                return NotFound();
            }
            var consultationRequest = await _context.ConsultationRequests.FindAsync(id);
            if (consultationRequest == null)
            {
                return NotFound();
            }

            _context.ConsultationRequests.Remove(consultationRequest);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ConsultationRequestExists(int? id)
        {
            return (_context.ConsultationRequests?.Any(e => e.ConsultationRequestId == id)).GetValueOrDefault();
        }
    }
}
