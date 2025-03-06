using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ASPNET_API.Domain.Entities;
using ASPNET_API.Domain.Interface.Repositories;
using static System.Net.Mime.MediaTypeNames;
using ASPNET_API.Application.Services.Interfa;
using ASPNET_API.Application.DTOs;
using ASPNET_API.Domain.Entities;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using ASPNET_API.Services;


namespace ASPNET_API.Application.Services
{
    public class CourseEnrollService : ICourseEnrollService
    {
        private readonly ICourseEnrollRepository _repository;
        private readonly IMapper _mapper;

        public CourseEnrollService(ICourseEnrollRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<CourseEnrollDTO>> GetAllCourseEnrollsAsync()
        {
            var courseEnrolls = await _repository.GetAllAsync();
            return _mapper.Map<List<CourseEnrollDTO>>(courseEnrolls);
        }

        public async Task<CourseEnrollDTO> GetCourseEnrollByCourseIdAsync(int? courseId, int userId)
        {
            var courseEnroll = await _repository.GetByCourseIdAsync(courseId, userId);
            return _mapper.Map<CourseEnrollDTO>(courseEnroll);
        }

        public async Task<CourseEnrollDTO> GetCourseEnrollByIdAsync(int id)
        {
            var courseEnroll = await _repository.GetByIdAsync(id);
            return _mapper.Map<CourseEnrollDTO>(courseEnroll);
        }

        public async Task<List<CourseEnrollDTO>> GetCourseEnrollsByCourseIdAsync(int? courseId)
        {
            var courseEnrolls = await _repository.GetByCourseIdsAsync(courseId);
            return _mapper.Map<List<CourseEnrollDTO>>(courseEnrolls);
        }

        public async Task<List<CourseEnrollDTO>> GetByUserIdAsync(int? userId)
        {
            var courseEnrolls = await _repository.GetByUserIdAsync(userId);
            return _mapper.Map<List<CourseEnrollDTO>>(courseEnrolls);
        }

        public async Task<bool> CourseEnrollExistsAsync(int id)
        {
            return await _repository.ExistsAsync(id);
        }

        public async Task AddCourseEnrollAsync(CourseEnrollDTO courseEnrollDto)
        {
            var courseEnroll = _mapper.Map<CourseEnroll>(courseEnrollDto);
            await _repository.AddAsync(courseEnroll);
        }

        public async Task UpdateCourseEnrollAsync(int id, CourseEnrollDTO courseEnrollDto)
        {
            var courseEnroll = _mapper.Map<CourseEnroll>(courseEnrollDto);
            await _repository.UpdateAsync(courseEnroll);
        }

        public async Task DeleteCourseEnrollAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
