using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElmnasaDomain.Entites.identity
{
    public class Student : Account
    {
        public long parent_number { get; set; }
        public long Uid { get; set; }
    }
}