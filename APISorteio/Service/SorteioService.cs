using APISorteio.Data.Repositories.Interfaces;
using APISorteio.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APISorteio.Service
{
    public class SorteioService
    {
        private IParticipanteRepository ParticipanteRepository;
        private ISorteioRepository SorteioRepository;
        private IParticipanteSorteioRepository ParticipanteSorteioRepository;
        public SorteioService(IParticipanteRepository participanteRepository, ISorteioRepository sorteioRepository,
            IParticipanteSorteioRepository participanteSorteioRepository)
        {
            ParticipanteRepository = participanteRepository;
            SorteioRepository = sorteioRepository;
            ParticipanteSorteioRepository = participanteSorteioRepository;
        }

        public async Task<List<Participante>> GetVencedoresSorteio(Sorteio sorteio)
        {
            IEnumerable<ParticipanteSorteio> participanteSorteios = await ParticipanteSorteioRepository.GetBySorteio(sorteio.SorteioId);
            List<Participante> participantesDoSorteio = new List<Participante>();
            List<Participante> vencedoresDoSorteio = new List<Participante>();
            
            foreach (ParticipanteSorteio ps in participanteSorteios)
            {
                Participante aux = await ParticipanteRepository.Get(ps.ParticipanteId);
                participantesDoSorteio.Add(aux);
            }

            for(int i = 1; i <= sorteio.NumeroDeGanhadores; i++)
            {
                Participante vencedor = Sortear(participantesDoSorteio);
                vencedoresDoSorteio.Add(vencedor);
            }

            return vencedoresDoSorteio; 
        }

        public Participante Sortear(List<Participante> participantes)
        {
            Random rand = new Random(DateTime.Now.Millisecond);
            Participante resultado = participantes[rand.Next(participantes.Count)];
            return resultado;
        }
    }
}
