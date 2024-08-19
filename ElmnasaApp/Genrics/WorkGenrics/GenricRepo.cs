using ElmnasaApp.Genrics.Intrefaces;
using ElmnasaApp.Specf.Intrefaces;
using ElmnasaApp.Specf.WorkSpecf;
using ElmnasaDomain.Entites.app;
using ElmnasaInfrastructure.AppContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElmnasaApp.Genrics.WorkGenrics
{
    public class GenricRepo<T> : IGenricRepo<T> where T : BaseEntity
    {
        private readonly ElmnasaContext _dbContext;

        public GenricRepo(ElmnasaContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(T item)
        => await _dbContext.Set<T>().AddAsync(item);

        public void DeleteAsync(T item)
        {
            _dbContext.Remove(item);
        }

        public async Task<IEnumerable<T>> GetAllWithAsync() => await _dbContext.Set<T>().ToListAsync();

        public async Task<T?> GetbyIdAsync(int id) => await _dbContext.Set<T>().FindAsync(id);

        public void Update(T item)
        {
            _dbContext.Update(item);
        }

        public async Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecifications<T> Spec)
        {
            return await GenerateSpec(Spec).ToListAsync();
        }

        private IQueryable<T> GenerateSpec(ISpecifications<T> Spec)
        {
            return SpecificationEvalutor<T>.GetQuery(_dbContext.Set<T>(), Spec).Result;
        }
    }
}