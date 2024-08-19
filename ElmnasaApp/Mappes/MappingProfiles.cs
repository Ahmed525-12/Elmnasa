using AutoMapper;
using ElmnasaDomain.DTOs.GradesDTO;
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
        }
    }
}