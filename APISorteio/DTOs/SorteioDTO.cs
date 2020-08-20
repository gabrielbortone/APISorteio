using APISorteio.Models;

namespace APISorteio.DTOs
{
    public class SorteioDTO
    {
        public int SorteioId { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string Premio { get; set; }
        public int NumeroDeGanhadores { get; set; }
        public Administrador Administrador { get; set; }
        public DataCompletaDTO DataFinalizacaoCadastro { get; set; }
        public DataCompletaDTO DataSorteio { get; set; }

        public SorteioDTO(string titulo, string descricao, string premio, int numeroDeGanhadores,
            DataCompletaDTO dataFinalizacaoCadastro, DataCompletaDTO dataSorteio )
        {
            Titulo = titulo;
            Descricao = descricao;
            Premio = premio;
            NumeroDeGanhadores = numeroDeGanhadores;
            DataFinalizacaoCadastro = dataFinalizacaoCadastro;
            DataSorteio = dataSorteio;
        }

        public SorteioDTO(int id, string titulo, string descricao, string premio, int numeroDeGanhadores,
            DataCompletaDTO dataFinalizacaoCadastro, DataCompletaDTO dataSorteio)
        {
            SorteioId = id;
            Titulo = titulo;
            Descricao = descricao;
            Premio = premio;
            NumeroDeGanhadores = numeroDeGanhadores;
            DataFinalizacaoCadastro = dataFinalizacaoCadastro;
            DataSorteio = dataSorteio;
        }

    }
}
