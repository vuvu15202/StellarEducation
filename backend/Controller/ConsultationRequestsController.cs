using ASPNET_API.Application.DTOs;
using ASPNET_API.Application.Services;
using ASPNET_API.Authorization;
using ASPNET_API.Domain.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace ASPNET_API.Controller
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultationRequestsController : ControllerBase
    {
        private readonly ConsultationRequestService _service;
        private readonly IMapper _mapper;

        public ConsultationRequestsController(ConsultationRequestService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [Authorize(RoleEnum.Admin, RoleEnum.Staff)]
        [HttpGet]
        public async Task<ActionResult> GetConsultationRequests()
        {
            var consultationRequests = await _service.GetAllConsultationRequestsAsync();
            return Ok(_mapper.Map<List<ConsultationRequestDTO>>(consultationRequests));
        }

        [Authorize(RoleEnum.Admin, RoleEnum.Staff)]
        [HttpGet("{id}")]
        public async Task<ActionResult<ConsultationRequest>> GetConsultationRequest(int? id)
        {
            var consultationRequest = await _service.GetConsultationRequestByIdAsync(id);
            if (consultationRequest == null) return NotFound();
            return Ok(consultationRequest);
        }

        [Authorize(RoleEnum.Admin, RoleEnum.Staff)]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutConsultationRequest(int? id, ConsultationRequest consultationRequest)
        {
            var user = HttpContext.Items["User"] as User;
            if (user == null) return Unauthorized();

            var result = await _service.UpdateConsultationRequestAsync(id, consultationRequest, user.UserId);
            if (!result) return BadRequest("Invalid ID");
            return NoContent();
        }

        [Authorize(RoleEnum.Admin, RoleEnum.Staff)]
        [HttpPut("changestatus/{id}/{status}")]
        public async Task<IActionResult> ChangeStatus(int? id, string? status)
        {
            var user = HttpContext.Items["User"] as User;
            if (user == null) return Unauthorized();

            var result = await _service.ChangeStatusAsync(id, status, user.UserId);
            if (!result) return NotFound("Không tìm thấy yêu cầu.");
            return NoContent();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<ConsultationRequest>> PostConsultationRequest(ConsultationRequest consultationRequest)
        {
            await _service.AddConsultationRequestAsync(consultationRequest);
            return CreatedAtAction(nameof(GetConsultationRequest), new { id = consultationRequest.ConsultationRequestId }, consultationRequest);
        }

        [HttpDelete("{id}")]
        [Authorize(RoleEnum.Admin, RoleEnum.Staff)]
        public async Task<IActionResult> DeleteConsultationRequest(int? id)
        {
            var result = await _service.DeleteConsultationRequestAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
