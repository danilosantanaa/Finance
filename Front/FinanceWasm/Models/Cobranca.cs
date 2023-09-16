using FinanceWasm.Helpers;
using FinanceWasm.Models.Enums.Cobrancas;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FinanceWasm.Models
{
    public class Cobranca
    {
        public int Id { get; set; }

        [Display(Name = "Descrição")]
        [Required(ErrorMessage = ModelMessageErro.REQUIRED)]
        public string Descricao { get; set; }

        [Display(Name = "Tipo")]
        [Required(ErrorMessage = ModelMessageErro.REQUIRED)]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public CobrancaEnums.TipoEnum? Tipo { get; set; } = null;

        [Display(Name = "Tipo Cobrança")]
        [Required(ErrorMessage = ModelMessageErro.REQUIRED)]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public CobrancaEnums.TipoCobrancaEnum? TipoCobranca { get; set; } = null;

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public CobrancaEnums.StatusEnum Status { get; set; } = CobrancaEnums.StatusEnum.Lancado;

        [Display(Name = "Quant. Parcelas")]
        public int QuantidadeParcelas { get; set; }

        public double Valor { get; set; }
        public DateTime DataEmissao { get; set; }

        [Required]
        [Display(Name = "Transacionador")]
        [Range(1, int.MaxValue, ErrorMessage = ModelMessageErro.REQUIRED)]
        public int TransacionadorId { get; set; }
        public Transacionador Transacionador { get; set; }

        public List<Recibo> Recibos { get; set; } = new();

        public static Dictionary<CobrancaEnums.TipoEnum, string> GetTipos() =>
        new Dictionary<CobrancaEnums.TipoEnum, string>() {
            { CobrancaEnums.TipoEnum.Pagar, "Pagar" },
            { CobrancaEnums.TipoEnum.Receber, "Receber" }
        };

        public static Dictionary<CobrancaEnums.TipoCobrancaEnum, string> GetTipoCobrancas() =>
        new Dictionary<CobrancaEnums.TipoCobrancaEnum, string>() {
            { CobrancaEnums.TipoCobrancaEnum.Mensal , "Mensal"},
            { CobrancaEnums.TipoCobrancaEnum.Avista, "À vista" },
            { CobrancaEnums.TipoCobrancaEnum.Indeterminado, "Indeterminado" }
        };

        public static Dictionary<CobrancaEnums.StatusEnum, string> GetStatus() =>
        new Dictionary<CobrancaEnums.StatusEnum, string>() {
            { CobrancaEnums.StatusEnum.Lancado, "Lançado" },
            { CobrancaEnums.StatusEnum.Cancelado, "Cancelado" },
            { CobrancaEnums.StatusEnum.Pago, "Pago" },
            { CobrancaEnums.StatusEnum.Concluir, "Concluído" }
        };
    }
}