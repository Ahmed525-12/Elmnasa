using AutoMapper;
using ElmnasaDomain.DTOs.GradesDTO;
using ElmnasaDomain.DTOs.SubjectDTOS;
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
        }
    }
}