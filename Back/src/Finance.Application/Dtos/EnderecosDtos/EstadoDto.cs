using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Finance.Application.Dtos.EnderecosDtos
{
    public class EstadoDto
    {
        public int Id { get; set; }
        public string Uf { get; set; }
        public string Nome { get; set; }
    }
}