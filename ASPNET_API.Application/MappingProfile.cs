using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ASPNET_API.Models.DTO;
//using X.PagedList;
using ASPNET_API.Domain.Entities;
using ASPNET_API.Application.DTOs;
using ASPNET_API.Application.DTOs.IELTS;

namespace ASPNET_API.Application
{
    public class MappingProfile : Profile
    {

        public MappingProfile()
        {
            //CreateMap<FundraisingProject, ProjectDTO>()
            //    .ForMember(p => p.Href, opt => opt.MapFrom(src => "#"))
            //    .ForMember(p => p.TypeText, opt => opt.MapFrom(src => Enum.GetName(typeof(TypeEnums), src.Type)))
            //    .ForMember(p => p.StartDateText, opt => opt.MapFrom(src => src.StartDate.ToShortDateString()))
            //    .ForMember(p => p.EndDateText, opt => opt.MapFrom(src => src.EndDate.ToShortDateString()))
            //    .ForMember(p => p.Total, opt => opt.MapFrom(src => src.Orders.Sum(x => Int32.Parse(x.Amount)))) 
            //    .ForMember(p => p.Status, opt => opt.MapFrom(src => Enum.GetName(typeof(ProjectStatusEnum), src.Discontinued)));

            CreateMap<StudentFee, StudentFeeDTO>()
                .ForMember(p => p.DateOfPaid, opt => opt.MapFrom(src => src.DateOfPaid!.Value.ToString("dd MMMM yyyy, 'at' hh:mm:ss tt")));

            CreateMap<CourseEnroll, CourseEnrollDTO>();
            CreateMap<User, UserDTO>();
            CreateMap<Course, CourseDTO>()
                .ForMember(x => x.NumberEnrolled, opt => opt.MapFrom(src => src.CourseEnrolls.Count()))
                .ForMember(x => x.TotalTime, opt => opt.MapFrom(src => src.Lessons != null ? src.Lessons.Sum(t => t.VideoTime) : 0))
				.ForMember(x => x.UpdatedAtString, opt => opt.MapFrom(src => src.UpdatedAt.Value.ToString("dd/MM/yyyy")));


            CreateMap<CreateCategory, Category>();
            CreateMap<UpdateCategory, Category>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<Comment, CommentDTO>();
            CreateMap<CreateComment, Comment>()
                .ForMember(x => x.CreateDate, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(x => x.IsHide, opt => opt.MapFrom(src => false));

            CreateMap<CreateReply, Reply>()
                .ForMember(x => x.CreateDate, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(x => x.IsHide, opt => opt.MapFrom(src => false));

            CreateMap<Reply, ReplyDTO>();

            CreateMap<Lesson, LessonDTO>().ForMember(dest => dest.Quiz, opt => opt.MapFrom(src => src.Quiz));

            CreateMap<QuestionBank, QuestionBankDTO>();
            CreateMap<ExamCandidate, ExamCandidateDTO>();
            CreateMap<ConsultationRequest, ConsultationRequestDTO>()
                .ForMember(
                    x => x.CreatedAtString,
                    opt => opt.MapFrom(
                        src => src.CreatedAt.HasValue
                            ? src.CreatedAt.Value.ToString("dd/MM/yyyy, 'at' hh:mm:ss tt")
                            : string.Empty
                    )
                );
            //CreateMap<QuestionBank, QuestionBankDTO>()
            //    .ForMember(dest => dest.ReadingString, opt => opt.MapFrom(src => src.Reading))
            //    .ForMember(dest => dest.ListeningString, opt => opt.MapFrom(src => src.Listening))
            //    .ForMember(dest => dest.WritingString, opt => opt.MapFrom(src => src.Writing));
            // Thêm các mappings khác nếu cần
        }
    }
    public static class AutoMapperConfig
    {
        public static IMapper InitializeAutoMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            return config.CreateMapper();
        }
    }
}
