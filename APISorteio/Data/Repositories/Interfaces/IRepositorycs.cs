using System.Linq;
using System.Threading.Tasks;

namespace APISorteio.Data.Repositories.Interfaces
{
    public interface IRepository<T>
    {
        Task<T> Get(int id);
        Task<IQueryable<T>> GetAll();
        Task<int> Add(T entity);
        Task<int> Delete(int id);
        Task<int> Update(T entity);
    }
}
