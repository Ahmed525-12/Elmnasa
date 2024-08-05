using ElmnasaApp.Genrics.Intrefaces;
using ElmnasaDomain.Entites.app;
using ElmnasaInfrastructure.AppContext;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElmnasaApp.Genrics.WorkGenrics
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ElmnasaContext _dbContext;
        private Hashtable Repo;

        public UnitOfWork(ElmnasaContext dbContext)
        {
            _dbContext = dbContext;
            Repo = new Hashtable();
        }

        public async Task<int> CompleteAsync()
            => await _dbContext.SaveChangesAsync();

        public ValueTask DisposeAsync()
        => _dbContext.DisposeAsync();

        public IGenricRepo<T> Repository<T>() where T : BaseEntity
        {
            var type = typeof(T).Name;
            if (!Repo.ContainsKey(type))
            {
                var repository = new GenricRepo<T>(_dbContext);
                Repo.Add(type, repository);
            }
            return (IGenricRepo<T>)Repo[type]!;
        }
    }
}