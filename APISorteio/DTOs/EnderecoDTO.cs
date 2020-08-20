namespace APISorteio.DTOs
{
    public class EnderecoDTO
    {
        public int EnderecoId { get; set; }
        public string Logradouro { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string Pais { get; set; }
        public EnderecoDTO(string logradouro, string bairro, string cidade, string estado, string pais)
        {
            Logradouro = logradouro;
            Bairro = bairro;
            Cidade = cidade;
            Estado = estado;
            Pais = pais;
        }
        public EnderecoDTO(int enderecoId, string logradouro, string bairro, string cidade, string estado, string pais)
        {
            EnderecoId = enderecoId;
            Logradouro = logradouro;
            Bairro = bairro;
            Cidade = cidade;
            Estado = estado;
            Pais = pais;
        }
    }
}
