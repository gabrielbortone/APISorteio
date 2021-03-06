﻿namespace APISorteio.DTOs
{
    public class ParticipanteDTO
    { 
        public int ParticipanteId { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }


        public ParticipanteDTO(string nome, string sobrenome, string email, string telefone)
        {
            Nome = nome;
            Sobrenome = sobrenome;
            Email = email;
            Telefone = telefone;
        }
        public ParticipanteDTO(int participanteId, string nome, string sobrenome, string email, string telefone)
        {
            ParticipanteId = participanteId;
            Nome = nome;
            Sobrenome = sobrenome;
            Email = email;
            Telefone = telefone;
        }
    }
}
