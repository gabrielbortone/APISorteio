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
    }
}
