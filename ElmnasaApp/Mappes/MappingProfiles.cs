using AutoMapper;
using ElmnasaDomain.DTOs.AnswerDtos;
using ElmnasaDomain.DTOs.GradesDTO;
using ElmnasaDomain.DTOs.QuestionDTO;
using ElmnasaDomain.DTOs.QuizDTOs;
using ElmnasaDomain.DTOs.SubjectDTOS;
using ElmnasaDomain.DTOs.SubscribeSubjectDTO;
using ElmnasaDomain.DTOs.TeacherSubjectDTOS;
using ElmnasaDomain.DTOs.UploadPdfDTOs;
using ElmnasaDomain.DTOs.UploadVideosDTOS;
using ElmnasaDomain.Entites.app;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElmnasaApp.Mappes
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Grades, GradeDto>().ReverseMap();
            CreateMap<Grades, UpdateGradeDTO>().ReverseMap();
            CreateMap<Subject, SubjectDTO>().ReverseMap();
            CreateMap<Subject, UpdateSubjectDto>().ReverseMap();
            CreateMap<SubscribeSubject, SubscribeSubjectReadDto>()
         .ForMember(dest => dest.Subjects, opt => opt.MapFrom(src => src.TeacherSubject));
            CreateMap<SubscribeSubjectCreateDto, SubscribeSubject>()
            .ForMember(dest => dest.TeacherSubject, opt => opt.Ignore());

            CreateMap<UploadPdf, UploadPdfReadDto>()
          .ForMember(dest => dest.SubjectIds, opt => opt.MapFrom(src => src.TeacherSubject.Select(s => s.Id)));

            CreateMap<UploadPdfCreateDto, UploadPdf>()
            .ForMember(dest => dest.TeacherSubject, opt => opt.Ignore());

            CreateMap<UploadVideo, UploadVideoCreateDto>()
     .ForMember(dest => dest.SubjectIds, opt => opt.MapFrom(src => src.TeacherSubject.Select(s => s.Id)));

            CreateMap<UploadVideoCreateDto, UploadVideo>()
                .ForMember(dest => dest.TeacherSubject, opt => opt.MapFrom(src => src.SubjectIds.Select(id => new Subject { Id = id })));

            // Mapping from UploadVideo to UploadVideoReadDto
            CreateMap<UploadVideo, UploadVideoReadDto>()
                .ForMember(dest => dest.SubjectIds, opt => opt.MapFrom(src => src.TeacherSubject.Select(s => s.Id)));

            // Mapping from UploadVideoReadDto to UploadVideo (optional if needed in reverse)
            CreateMap<UploadVideoReadDto, UploadVideo>()
                .ForMember(dest => dest.TeacherSubject, opt => opt.Ignore()); // You can ignore or map as needed

            //___________________________________________________

            // Map Question to QuestionCreateDto, where Answers in Question is mapped to a list of Answer IDs
            CreateMap<Question, QuestionCreateDto>()
                .ForMember(dest => dest.AnswerIds, opt => opt.MapFrom(src => src.Answers.Select(s => s.Id)));

            // Map QuestionCreateDto to Question, where AnswerIds in the DTO are mapped back to Answer objects in Question
            CreateMap<QuestionCreateDto, Question>()
                .ForMember(dest => dest.Answers, opt => opt.MapFrom(src => src.AnswerIds.Select(a => new Answer { Id = a })));

            // Map Question to QuestionReadDto, where Answers in Question are mapped to AnswerDTO objects
            CreateMap<Question, QuestionReadDto>()
                .ForMember(dest => dest.Answers, opt => opt.MapFrom(src => src.Answers));

            // Map QuestionReadDto to Question, where the Answer mapping is ignored (you can handle this separately)
            CreateMap<QuestionReadDto, Question>()
                .ForMember(dest => dest.Answers, opt => opt.Ignore());

            // Map Quiz to QuizReadDto
            CreateMap<Quiz, QuizReadDto>()
                .ForMember(dest => dest.Question, opt => opt.MapFrom(src => src.Question))
                .ForMember(dest => dest.Subject, opt => opt.MapFrom(src => src.TeacherSubject));

            // Map QuizCreateDTO to Quiz
            CreateMap<QuizCreateDTO, Quiz>()
                .ForMember(dest => dest.Question, opt => opt.MapFrom(src => src.QuestionIds.Select(id => new Question { Id = id })))
                .ForMember(dest => dest.TeacherSubject, opt => opt.MapFrom(src => src.SubjectIds.Select(id => new Subject { Id = id })));

            // Map QuizReadDto to Quiz (usually needed when updating)
            CreateMap<QuizReadDto, Quiz>()
                .ForMember(dest => dest.Question, opt => opt.Ignore()) // Optionally handle separately if needed
                .ForMember(dest => dest.TeacherSubject, opt => opt.Ignore()); // Optionally handle separately if needed

            CreateMap<Answer, AnswerDTO>();

            CreateMap<TeacherSubject, TeacherSubjectReadDto>()
           .ForMember(dest => dest.Subject, opt => opt.MapFrom(src => src.Subject));

            CreateMap<TeacherSubjectCreateDto, TeacherSubject>()
           .ForMember(dest => dest.Subject, opt => opt.Ignore()) // Ignore if not needed during creation
           .ForMember(dest => dest.Teacher_id, opt => opt.Ignore()); // Ignore if not provided during creation
        }
    }
}