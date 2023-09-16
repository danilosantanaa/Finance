namespace FinanceWasm.Models.Enums.Cobrancas {
    public static class CobrancaEnums {
        public enum TipoEnum {
            Pagar,
            Receber
        }

        public enum TipoCobrancaEnum {
            Mensal,
            Avista,
            Indeterminado
        }

        public enum StatusEnum {
            Lancado,
            Cancelado,
            Pago,
            Concluir
        }
    }
}
