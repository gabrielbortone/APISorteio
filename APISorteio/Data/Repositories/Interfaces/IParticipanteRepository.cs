using APISorteio.Models;

namespace APISorteio.Data.Repositories.Interfaces
{
    public interface IParticipanteRepository : IRepository<Participante>
    {
        public Participante GetParticipanteByCPF(string _CPF);
    }
}
