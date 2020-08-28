using System;
using System.ComponentModel.DataAnnotations;

namespace APISorteio.DTOs
{
    public class DataCompletaDTO
    {
        public DataCompletaDTO(int ano, int mes, int dia, int hora, int minuto)
        {
            Ano = ano;
            Mes = mes;
            Dia = dia;
            Hora = hora;
            Minuto = minuto;
        }

        [Required]
        [Range(2000, 2020)]
        public int Ano { get; set; }
        [Required]
        [Range(1, 12)]
        public int Mes { get; set; }
        [Required]
        [Range(1, 31)]
        public int Dia { get; set; }
        [Required]
        [Range(0, 23)]
        public int Hora { get; set; }
        [Required]
        [Range(0, 59)]
        public int Minuto { get; set; }
    }
}
