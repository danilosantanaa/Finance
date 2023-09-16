using FinanceWasm.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceWasm.Models
{
    public class Banco
    {
        public int Id { get; set; }

        [Required(ErrorMessage = ModelMessageErro.REQUIRED)]
        [MinLength(3, ErrorMessage = ModelMessageErro.MIN_STR_LENGTH)]
        public string Nome { get; set; } = string.Empty;
        public bool Status { get; set; } = true;
    }
}