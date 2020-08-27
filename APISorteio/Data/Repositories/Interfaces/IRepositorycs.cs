using System.Collections.Generic;
using System.Threading.Tasks;

namespace APISorteio.Data.Repositories.Interfaces
{
    public interface IRepository<T>
    {
        Task<T> Get(int id);
        Task<IEnumerable<T>> GetAll();
        Task<int> Add(T entity);
        Task<int> Delete(int id);
        Task<int> Update(T entity);
    }
}
