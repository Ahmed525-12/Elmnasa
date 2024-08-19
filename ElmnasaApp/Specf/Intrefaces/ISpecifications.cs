using ElmnasaDomain.Entites.app;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ElmnasaApp.Specf.Intrefaces
{
    public interface ISpecifications<T> where T : BaseEntity
    {
        public Expression<Func<T, bool>> Criteria { get; set; }

        // Sign For Property For Include [Include(P => P.ProductType).Include(P => P.ProductBrand)]
        public List<Expression<Func<T, object>>> Includes { get; set; }
    }
}