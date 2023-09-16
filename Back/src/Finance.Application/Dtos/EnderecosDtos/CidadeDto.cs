using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Finance.Application.Dtos.EnderecosDtos
{
    public class CidadeDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sigla { get; set; }
        public int EstadoId { get; set; }
        public EstadoDto Estado { get; set; }
    }
}