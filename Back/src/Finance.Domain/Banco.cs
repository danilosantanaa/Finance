using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Finance.Domain.Identity;

namespace Finance.Domain
{
    public class Banco : Entity
    {

        [Required]
        public string Nome { get; set; } = string.Empty;
        public bool Status { get; set; }

        public IEnumerable<Recibo> Recibos { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}