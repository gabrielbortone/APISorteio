using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APISorteio.Models
{
    public class Participante
    {
        public int ParticipanteId { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string CPF { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public Endereco Endereco { get; set; }
    }
}
