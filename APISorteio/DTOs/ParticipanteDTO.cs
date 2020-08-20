﻿namespace APISorteio.DTOs
{
    public class ParticipanteDTO
    { 
        public int ParticipanteId { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string CPF { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public EnderecoDTO EnderecoDTO { get; set; }

        public ParticipanteDTO(string nome, string sobrenome, string cPF, string email,
            string telefone, EnderecoDTO enderecoDTO)
        {
            Nome = nome;
            Sobrenome = sobrenome;
            CPF = cPF;
            Email = email;
            Telefone = telefone;
            EnderecoDTO = enderecoDTO;
        }
        public ParticipanteDTO(int participanteId, string nome, string sobrenome, string cPF, string email, 
            string telefone, EnderecoDTO enderecoDTO)
        {
            ParticipanteId = participanteId;
            Nome = nome;
            Sobrenome = sobrenome;
            CPF = cPF;
            Email = email;
            Telefone = telefone;
            EnderecoDTO = enderecoDTO;
        }
    }
}
