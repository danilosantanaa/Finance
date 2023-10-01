using Finance.Application.Dtos.CategoriaDtos;
using Finance.Application.Dtos.CobrancaDtos;
using Finance.Application.Dtos.EnderecosDtos;

namespace Finance.Application.Dtos.TransacionadoresDtos;

public class TransacionadorResponseDto
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }

    public string Telefone { get; set; }

    public bool Status { get; set; }

    public IEnumerable<CobrancaDto> Cobrancas { get; set; }

    public int CategoriaId { get; set; }
    public CategoriaDto Categoria { get; set; }

    public EnderecoResponseDto Endereco { get; set; }
}