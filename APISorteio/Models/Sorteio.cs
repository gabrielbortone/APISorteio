using System;

namespace APISorteio.Models
{
    public class Sorteio
    {
        public int SorteioId { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string Premio { get; set; }
        public int NumeroDeGanhadores { get; set; }
        public Administrador Administrador { get; set; }
        public DateTime DataFinalizacaoCadastro { get; set; }
        public DateTime DataSorteio { get; set; }

        public Sorteio(string titulo, string descricao, string premio, int numeroDeGanhadores, 
            Administrador administrador, DateTime dataFinalizacaoCadastro, DateTime dataSorteio)
        {
            Titulo = titulo;
            Descricao = descricao;
            Premio = premio;
            NumeroDeGanhadores = numeroDeGanhadores;
            Administrador = administrador;
            DataFinalizacaoCadastro = dataFinalizacaoCadastro;
            DataSorteio = dataSorteio;
        }

        public Sorteio(int sorteioId, string titulo, string descricao, string premio, int numeroDeGanhadores, 
            Administrador administrador, DateTime dataFinalizacaoCadastro, DateTime dataSorteio)
        {
            SorteioId = sorteioId;
            Titulo = titulo;
            Descricao = descricao;
            Premio = premio;
            NumeroDeGanhadores = numeroDeGanhadores;
            Administrador = administrador;
            DataFinalizacaoCadastro = dataFinalizacaoCadastro;
            DataSorteio = dataSorteio;
        }
    }
}
