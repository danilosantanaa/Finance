using System.ComponentModel.DataAnnotations;
using Finance.Application.Attributes;

namespace Finance.Application.Dtos.CategoriaDtos;

public class CategoriaDto
{
    public int Id { get; set; }

    [Required]
    [TreatSpaces]
    public string Descricao { get; set; }
    public bool Status { get; set; }
}