﻿using ElmnasaApp.Specf.Intrefaces;
using ElmnasaDomain.Entites.app;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElmnasaApp.Specf.AppSpecf.AnswerSpecf
{
    public class UserAnswerSpecf : BaseSpecifications<Answer>
    {
        public UserAnswerSpecf(string userId) : base(p => p.Teacher_id == userId

            )
        {
        }
    }
}