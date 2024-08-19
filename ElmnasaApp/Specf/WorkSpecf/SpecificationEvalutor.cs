using ElmnasaApp.Specf.Intrefaces;
using ElmnasaDomain.Entites.app;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElmnasaApp.Specf.WorkSpecf
{
    public static class SpecificationEvalutor<T> where T : BaseEntity
    {
        //_dbContext.Products.Where(P => P.Id == id).Include(P => P.ProductType).Include(P => P.ProductBrand);

        public static async Task<IQueryable<T>> GetQuery(IQueryable<T> InputQuery, ISpecifications<T> Spec)
        {
            var Query = InputQuery;

            //.Where(P => P.Id == id)
            if (Spec.Criteria is not null)
                Query = Query.Where(Spec.Criteria);

            Query = Spec.Includes.Aggregate(Query, (CurrentQuery, IncludeExpression) => CurrentQuery.Include(IncludeExpression));

            return Query;
        }
    }
}