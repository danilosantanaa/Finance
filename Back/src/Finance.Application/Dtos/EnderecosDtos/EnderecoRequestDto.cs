namespace Finance.Application.Dtos.EnderecosDtos;

public class EnderecoRequestDto
{
    public int Id { get; set; }
    public string Cep { get; set; }
    public string Logradouro { get; set; }
    public string Complemento { get; set; }
    public string Bairro { get; set; }
    public int CidadeId { get; set; }
}