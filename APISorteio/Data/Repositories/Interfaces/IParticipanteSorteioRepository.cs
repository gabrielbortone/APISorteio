using APISorteio.Models;
using System.Threading.Tasks;

namespace APISorteio.Data.Repositories.Interfaces
{
    public interface IParticipanteSorteioRepository : IRepository<ParticipanteSorteio>
    {
        public Task<ParticipanteSorteio> GetBySorteio(int id);
        public Task<ParticipanteSorteio> GetByParticipante(int id);
    }
}
