using APISorteio.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APISorteio.Data.Repositories.Interfaces
{
    public interface IParticipanteSorteioRepository : IRepository<ParticipanteSorteio>
    {
        public Task<IEnumerable<ParticipanteSorteio>> GetBySorteio(int id);
        public Task<IEnumerable<ParticipanteSorteio>> GetByParticipante(int id);
    }
}
