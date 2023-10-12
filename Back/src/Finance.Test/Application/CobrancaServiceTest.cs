using AutoMapper;
using Finance.Application.Contratos;
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
    IMapper _mapper;

    public CobrancaServiceTest()
    {

        var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new FinanceProfile());
            });

        _mapper = mapperConfig.CreateMapper();
    }

    [Fact]
    public async Task Verificar_Add_Cobranca_Com_Status_Do_Tipo_Lancada()
    {
        var cobrancaPersistenceMock = new Mock<ICobrancaPersistence>();
        cobrancaPersistenceMock
            .Setup(service => service.Add(It.IsAny<Cobranca>()));

        cobrancaPersistenceMock
            .Setup(service => service.SaveChangesAsync())
            .ReturnsAsync(true);

        cobrancaPersistenceMock
            .Setup(service => service.GetByIdAsync(1, 1, false))
            .ReturnsAsync(new Cobranca());

        CobrancaService cobrancaService = new CobrancaService(
            cobrancaPersistenceMock.Object,
            _mapper
        );

        var result = await cobrancaService.AddAsync(1, CobrancaFixture.GetCobrancaDtoAvista());

        result.Status
            .Should()
            .Be(CobrancaStatusEnum.Lancado.ToString());
    }

    [Fact]
    public async Task Verificar_Add_Cobranca_Quando_Cobranca_For_Mensal_Com_Qtd_Parcela_Zerado()
    {
        var cobrancaPersistenceMock = new Mock<ICobrancaPersistence>();

        cobrancaPersistenceMock
           .Setup(service => service.Add(It.IsAny<Cobranca>()));

        cobrancaPersistenceMock
            .Setup(service => service.SaveChangesAsync())
            .ReturnsAsync(true);

        cobrancaPersistenceMock
            .Setup(service => service.GetByIdAsync(1, 1, false))
            .ReturnsAsync(new Cobranca());

        CobrancaService cobrancaService = new CobrancaService(
            cobrancaPersistenceMock.Object,
            _mapper
        );

        Func<Task> action = async () => await cobrancaService.AddAsync(1, CobrancaFixture.GetCobrancaDtoMensalComTotalZerado());

        await action
            .Should()
            .ThrowAsync<ExceptionServiceBadRequestError>()
            .WithMessage("Para cobrança do tipo Mensal, deve-se informar a Quantidade.");
    }

    [Fact]
    public async Task Verificar_Add_Cobranca_Quando_Cobranca_For_Mensal_Com_Valor_Zerado()
    {
        var cobrancaPersistenceMock = new Mock<ICobrancaPersistence>();

        cobrancaPersistenceMock
           .Setup(service => service.Add(It.IsAny<Cobranca>()));

        cobrancaPersistenceMock
            .Setup(service => service.SaveChangesAsync())
            .ReturnsAsync(true);

        cobrancaPersistenceMock
            .Setup(service => service.GetByIdAsync(1, 1, false))
            .ReturnsAsync(new Cobranca());

        CobrancaService cobrancaService = new CobrancaService(
            cobrancaPersistenceMock.Object,
            _mapper
        );

        Func<Task> action =
            async () => await cobrancaService.AddAsync(
                1,
                CobrancaFixture.GetCobrancaDtoMensalComValorZerado()
            );

        await action
            .Should()
            .ThrowAsync<ExceptionServiceBadRequestError>()
            .WithMessage("Para cobrança do tipo Mensal, o Valor não pode ser zerado.");
    }

    [Fact]
    public async Task Verificar_Update_Cobranca_Quando_Nao_Encontrado_A_Cobranca()
    {
        var cobrancaPersistenceMock = new Mock<ICobrancaPersistence>();

        cobrancaPersistenceMock
            .Setup(service => service.GetByIdAsync(1, 1, false))
            .Returns(Task.FromResult((Cobranca?)null));

        var cobrancaService = new CobrancaService(cobrancaPersistenceMock.Object, _mapper);

        var result =
            await cobrancaService.UpdateAsync(
                1,
                1,
                CobrancaFixture.GetCobrancaDtoAvista()
            );

        result.Should().BeNull();
    }

    [Fact]
    public async Task Verificar_Cobranca_Nao_Pode_Ser_Atualizado_Quando_Nao_For_Do_Status_Lancado()
    {
        var cobrancaPersistenceMock = new Mock<ICobrancaPersistence>();

        cobrancaPersistenceMock
            .Setup(service => service.GetByIdAsync(1, 1, false))
            .ReturnsAsync(new Cobranca()
            {
                Status = CobrancaStatusEnum.Pago
            });

        var cobrancaService = new CobrancaService(cobrancaPersistenceMock.Object, _mapper);

        Func<Task> action =
            async () => await cobrancaService.UpdateAsync(
                1,
                1,
                CobrancaFixture.GetCobrancaDtoMensal()
            );

        await action
            .Should()
            .ThrowAsync<ExceptionServiceBadRequestError>()
            .WithMessage("Não é permitido editar a cobrança.");
    }

    [Fact]
    public async Task Verificar_Update_Cobranca_Quando_Cobranca_For_Mensal_Com_Qtd_Parcela_Zerado()
    {
        var cobrancaPersistenceMock = new Mock<ICobrancaPersistence>();

        cobrancaPersistenceMock
           .Setup(service => service.Add(It.IsAny<Cobranca>()));

        cobrancaPersistenceMock
            .Setup(service => service.SaveChangesAsync())
            .ReturnsAsync(true);

        cobrancaPersistenceMock
            .Setup(service => service.GetByIdAsync(1, 1, false))
            .ReturnsAsync(new Cobranca());

        CobrancaService cobrancaService = new CobrancaService(
            cobrancaPersistenceMock.Object,
            _mapper
        );

        Func<Task> action =
            async () => await cobrancaService.UpdateAsync(
                1,
                1,
                CobrancaFixture.GetCobrancaDtoMensalComTotalZerado()
            );

        await action
            .Should()
            .ThrowAsync<ExceptionServiceBadRequestError>()
            .WithMessage("Para cobrança do tipo Mensal, deve-se informar a Quantidade.");
    }

    [Fact]
    public async Task Verificar_Update_Cobranca_Quando_Cobranca_For_Mensal_Com_Valor_Zerado()
    {
        var cobrancaPersistenceMock = new Mock<ICobrancaPersistence>();

        cobrancaPersistenceMock
           .Setup(service => service.Add(It.IsAny<Cobranca>()));

        cobrancaPersistenceMock
            .Setup(service => service.SaveChangesAsync())
            .ReturnsAsync(true);

        cobrancaPersistenceMock
            .Setup(service => service.GetByIdAsync(1, 1, false))
            .ReturnsAsync(new Cobranca());

        CobrancaService cobrancaService = new CobrancaService(
            cobrancaPersistenceMock.Object,
            _mapper
        );

        Func<Task> action =
            async () => await cobrancaService.UpdateAsync(
                1,
                1,
                CobrancaFixture.GetCobrancaDtoMensalComValorZerado()
            );

        await action
            .Should()
            .ThrowAsync<ExceptionServiceBadRequestError>()
            .WithMessage("Para cobrança do tipo Mensal, o Valor não pode ser zerado.");
    }
}
