using Newtonsoft.Json;

namespace APISorteio.DTOs
{
    public class ParticipanteCadastroDTO
    {
        public int ParticipanteId { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string CPF { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Logradouro { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string Pais { get; set; }

        

        public ParticipanteCadastroDTO(int participanteId, string nome, string sobrenome, string cPF, string email,
            string telefone)
        {
            ParticipanteId = participanteId;
            Nome = nome;
            Sobrenome = sobrenome;
            CPF = cPF;
            Email = email;
            Telefone = telefone;
        }

        [JsonConstructor]
        public ParticipanteCadastroDTO(string nome, string sobrenome, string cPF, string email, 
            string telefone, string logradouro, string bairro, string cidade, string estado, string pais)
        {
            Nome = nome;
            Sobrenome = sobrenome;
            CPF = cPF;
            Email = email;
            Telefone = telefone;
            Logradouro = logradouro;
            Bairro = bairro;
            Cidade = cidade;
            Estado = estado;
            Pais = pais;
        }
    }
}
