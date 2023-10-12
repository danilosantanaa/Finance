using Finance.Application.Dtos.CobrancaDtos;
using Finance.Domain.Enum;

namespace Finance.Test;

public static class CobrancaFixture
{
    public static CobrancaDto GetCobrancaDtoAvista() => new CobrancaDto
    {
        Descricao = "Cobranca À vista",
        Valor = 40,
        TipoCobranca = "Avista"
    };

    public static CobrancaDto GetCobrancaDtoMensal() => new CobrancaDto
    {
        Descricao = "Cobranca Mensal",
        Valor = 1000,
        QuantidadeParcelas = 10,
        TipoCobranca = "Mensal"
    };

    public static CobrancaDto GetCobrancaDtoMensalComTotalZerado() => new CobrancaDto
    {
        Descricao = "Cobranca Mensal",
        Valor = 1000,
        QuantidadeParcelas = 0,
        TipoCobranca = "Mensal"
    };

    public static CobrancaDto GetCobrancaDtoMensalComValorZerado() => new CobrancaDto
    {
        Descricao = "Cobranca Mensal",
        Valor = 0,
        QuantidadeParcelas = 10,
        TipoCobranca = "Mensal"
    };

    public static CobrancaDto GetCobrancaDtoStatusPago() => new CobrancaDto
    {
        Descricao = "Cobrança Paga",
        Valor = 500,
        TipoCobranca = TipoCobrancaEnum.Avista.ToString(),
        Status = CobrancaStatusEnum.Pago.ToString()
    };
}
