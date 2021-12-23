using desafio_fullstack.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace desafio_fullstack.Repository
{
    public interface IRepository<T> where T : Entity
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(long id);
        Task Save(T entity);
        Task Update(T entity);
        Task Delete(T entity);
    }
}
