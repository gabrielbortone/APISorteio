using System;
using System.ComponentModel.DataAnnotations;

namespace APISorteio.DTOs
{
    public class DataResumoDTO
    {
        [Required]
        [Range(2000, 2020)]
        public int Ano { get; set; }
        [Required]
        [Range(1, 12)]
        public int Mes { get; set; }
        [Required]
        [Range(1, 31)]
        public int Dia { get; set; }
    }
}
