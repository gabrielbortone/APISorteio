using APISorteio.Models;
using System.Threading.Tasks;

namespace APISorteio.Data.Repositories.Interfaces
{
    public interface IParticipanteRepository : IRepository<Participante>
    {
        public Task<Participante> GetParticipanteByCPF(string _CPF);
    }
}
