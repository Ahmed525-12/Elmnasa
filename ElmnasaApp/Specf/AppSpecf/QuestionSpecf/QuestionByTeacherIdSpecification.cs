﻿using ElmnasaApp.Specf.Intrefaces;
using ElmnasaDomain.Entites.app;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElmnasaApp.Specf.AppSpecf.QuestionSpecf
{
    public class QuestionByTeacherIdSpecification : BaseSpecifications<Question>
    {
        public QuestionByTeacherIdSpecification(string Student_id)
            : base(ss => ss.Teacher_id == Student_id)
        {
            // Optionally, include related entities like Answers or Teacher if needed
            Includes.Add(ss => ss.Answers);
        }
    }
}