namespace APISorteio.DTOs
{
    public class ParticipanteCompletoDTO
    {
        public int ParticipanteId { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string CPF { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public ParticipanteCompletoDTO(string nome, string sobrenome, string cPF, string email, string telefone)
        {
            Nome = nome;
            Sobrenome = sobrenome;
            CPF = cPF;
            Email = email;
            Telefone = telefone;
        }
        public ParticipanteCompletoDTO(int participanteId, string nome, string sobrenome, string cPF, string email, string telefone)
        {
            ParticipanteId = participanteId;
            Nome = nome;
            Sobrenome = sobrenome;
            CPF = cPF;
            Email = email;
            Telefone = telefone;
        }
    }
}
