using AutoMapper;
using ElmnasaDomain.DTOs.GradesDTO;
using ElmnasaDomain.DTOs.SubjectDTOS;
using ElmnasaDomain.DTOs.SubscribeSubjectDTO;
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
         .ForMember(dest => dest.Subjects, opt => opt.MapFrom(src => src.Subject));
            CreateMap<SubscribeSubjectCreateDto, SubscribeSubject>()
            .ForMember(dest => dest.Subject, opt => opt.Ignore());

            CreateMap<UploadPdf, UploadPdfReadDto>()
          .ForMember(dest => dest.SubjectIds, opt => opt.MapFrom(src => src.Subject.Select(s => s.Id)));

            CreateMap<UploadPdfCreateDto, UploadPdf>()
            .ForMember(dest => dest.Subject, opt => opt.Ignore());

            CreateMap<UploadVideo, UploadVideoCreateDto>()
     .ForMember(dest => dest.SubjectIds, opt => opt.MapFrom(src => src.Subject.Select(s => s.Id)));

            CreateMap<UploadVideoCreateDto, UploadVideo>()
                .ForMember(dest => dest.Subject, opt => opt.MapFrom(src => src.SubjectIds.Select(id => new Subject { Id = id })));

            // Mapping from UploadVideo to UploadVideoReadDto
            CreateMap<UploadVideo, UploadVideoReadDto>()
                .ForMember(dest => dest.SubjectIds, opt => opt.MapFrom(src => src.Subject.Select(s => s.Id)));

            // Mapping from UploadVideoReadDto to UploadVideo (optional if needed in reverse)
            CreateMap<UploadVideoReadDto, UploadVideo>()
                .ForMember(dest => dest.Subject, opt => opt.Ignore()); // You can ignore or map as needed
        }
    }
}