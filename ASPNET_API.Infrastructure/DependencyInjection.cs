using ASPNET_API.Domain.Interface.Repositories;
using ASPNET_API.Infrastructure.Data;
using ASPNET_API.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ASPNET_API.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<DonationWebApp_v2Context>(option => option.UseSqlServer(connectionString));
            services.AddScoped<DonationWebApp_v2Context>();

            //repository
            services.AddScoped<IConsultationRequestRepository, ConsultationRequestRepository>();
            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<ILessonRepository, LessonRepository>();
            services.AddScoped<IQuestionBankRepository, QuestionBankRepository>();
            services.AddScoped<ICourseEnrollRepository, CourseEnrollRepository>();
            services.AddScoped<IExamCandidateRepository, ExamCandidateRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IUserRoleRepository, UserRoleRepository>();


            return services;
        }
    }
}
