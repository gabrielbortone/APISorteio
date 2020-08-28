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
        public Participante(string nome, string sobrenome, string cPF, string email, string telefone, Endereco endereco)
        {
            Nome = nome;
            Sobrenome = sobrenome;
            CPF = cPF;
            Email = email;
            Telefone = telefone;
            Endereco = endereco;
        }
        public Participante(int participanteId, string nome, string sobrenome, string cPF, string email, string telefone, Endereco endereco)
        {
            ParticipanteId = participanteId;
            Nome = nome;
            Sobrenome = sobrenome;
            CPF = cPF;
            Email = email;
            Telefone = telefone;
            Endereco = endereco;
        }

        public Participante(string nome, string sobrenome, string cPF, string email, string telefone)
        {
            Nome = nome;
            Sobrenome = sobrenome;
            CPF = cPF;
            Email = email;
            Telefone = telefone;
        }
    }
}
