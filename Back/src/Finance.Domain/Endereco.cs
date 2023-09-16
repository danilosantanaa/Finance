namespace Finance.Domain
{
    public class Endereco
    {
        public int Id { get; set; }
        public string Cep { get; set; }
        public string Logradouro { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public int CidadeId { get; set; }
        public Cidade Cidade { get; set; }

        public int? TransacionadorId { get; set; }
        public Transacionador? Transacionador { get; set; }
    }
}