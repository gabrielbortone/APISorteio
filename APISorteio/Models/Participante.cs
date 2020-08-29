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
        public int Id_Endereco { get; set; }
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

        public Participante(string nome, string sobrenome, string cPF, string email, string telefone, int id_Endereco)
        {
            Nome = nome;
            Sobrenome = sobrenome;
            CPF = cPF;
            Email = email;
            Telefone = telefone;
            Id_Endereco = id_Endereco;
        }

        public Participante(int participanteId, string nome, string sobrenome, string cPF, string email, string telefone, int id_Endereco)
        {
            ParticipanteId = participanteId;
            Nome = nome;
            Sobrenome = sobrenome;
            CPF = cPF;
            Email = email;
            Telefone = telefone;
            Id_Endereco = id_Endereco;
        }
    }
}
