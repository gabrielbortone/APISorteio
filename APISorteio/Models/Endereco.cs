using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APISorteio.Models
{
    public class Endereco
    {
        public int EnderecoId { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string País { get; set; }
    }
}
