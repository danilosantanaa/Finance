using FinanceWasm.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FinanceWasm.Models
{
    public enum StatusRecibo { Ativo, Cancelado }
    public class Recibo
    {
        public int Id { get; set; }
        public double Valor { get; set; }
        public double Desconto { get; set; }

        [Display(Name = "Acréscimo")]
        public double Acrescimo { get; set; }

        public DateTime DataPagamento { get; set; }

        public DateTime DataCancelamento { get; set; }
        public string Obs { get; set; }

        [Display(Name = "Banco")]
        [Range(1, int.MaxValue, ErrorMessage = ModelMessageErro.REQUIRED)]
        public int BancoId { get; set; }

        public Banco Banco { get; set; }

        public int CobrancaId { get; set; }
        public int UserId { get; set; }


        [JsonConverter(typeof(JsonStringEnumConverter))]
        public StatusRecibo Status { get; set; }
    }
}