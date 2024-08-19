using ElmnasaApp.Specf.Intrefaces;
using ElmnasaDomain.Entites.app;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElmnasaApp.Genrics.Intrefaces
{
    public interface IGenricRepo<T> where T : BaseEntity
    {
        Task<T> GetbyIdAsync(int id);

        Task<IEnumerable<T>> GetAllWithAsync();

        Task AddAsync(T item);

        void DeleteAsync(T item);

        void Update(T item);

        Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecifications<T> Spec);
    }
}