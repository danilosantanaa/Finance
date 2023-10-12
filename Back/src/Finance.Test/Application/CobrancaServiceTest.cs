using AutoMapper;
using Finance.Application.Helpers;
using Finance.Application.Services;
using Finance.Domain;
using Finance.Domain.Enum;
using Finance.Persistence.Contratos;
using FluentAssertions;
using Moq;

namespace Finance.Test;

public class CobrancaServiceTest
{
    Mock<ICobrancaPersistence> _cobrancaPersistenceMock;
    IMapper _mapper;

    public CobrancaServiceTest()
    {
        _cobrancaPersistenceMock = new Mock<ICobrancaPersistence>();
        var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new FinanceProfile());
            });

        _mapper = mapperConfig.CreateMapper();
    }

    [Fact]
    public async Task Verificar_Criacao_Cobranca_Com_Status_Do_Tipo_Lancada()
    {
        _cobrancaPersistenceMock
            .Setup(service => service.Add(It.IsAny<Cobranca>()));

        _cobrancaPersistenceMock
            .Setup(service => service.SaveChangesAsync())
            .ReturnsAsync(true);

        _cobrancaPersistenceMock
            .Setup(service => service.GetByIdAsync(1, 1, false))
            .ReturnsAsync(new Cobranca());

        CobrancaService cobrancaService = new CobrancaService(
            _cobrancaPersistenceMock.Object,
            _mapper
        );

        var result = await cobrancaService.AddAsync(1, CobrancaFixture.GetCobrancaDtoAvista());

        result.Status
            .Should()
            .Be(CobrancaStatusEnum.Lancado.ToString());
    }

    [Fact]
    public async Task Verificar_Quando_Cobranca_For_Mensal_Com_Qtd_Parcela_Zerado()
    {
        _cobrancaPersistenceMock
           .Setup(service => service.Add(It.IsAny<Cobranca>()));

        _cobrancaPersistenceMock
            .Setup(service => service.SaveChangesAsync())
            .ReturnsAsync(true);

        _cobrancaPersistenceMock
            .Setup(service => service.GetByIdAsync(1, 1, false))
            .ReturnsAsync(new Cobranca());

        CobrancaService cobrancaService = new CobrancaService(
            _cobrancaPersistenceMock.Object,
            _mapper
        );

        Func<Task> action = async () => await cobrancaService.AddAsync(1, CobrancaFixture.GetCobrancaDtoMensalComTotalZerado());

        await action
            .Should()
            .ThrowAsync<ExceptionServiceBadRequestError>()
            .WithMessage("Para cobrança do tipo Mensal, deve-se informar a Quantidade.");
    }

    [Fact]
    public async Task Verificar_Quando_Cobranca_For_Mensal_Com_Valor_Zerado()
    {
        _cobrancaPersistenceMock
           .Setup(service => service.Add(It.IsAny<Cobranca>()));

        _cobrancaPersistenceMock
            .Setup(service => service.SaveChangesAsync())
            .ReturnsAsync(true);

        _cobrancaPersistenceMock
            .Setup(service => service.GetByIdAsync(1, 1, false))
            .ReturnsAsync(new Cobranca());

        CobrancaService cobrancaService = new CobrancaService(
            _cobrancaPersistenceMock.Object,
            _mapper
        );

        Func<Task> action = async () => await cobrancaService.AddAsync(1, CobrancaFixture.GetCobrancaDtoMensalComValorZerado());

        await action
            .Should()
            .ThrowAsync<ExceptionServiceBadRequestError>()
            .WithMessage("Para cobrança do tipo Mensal, o Valor não pode ser zerado.");
    }
}
