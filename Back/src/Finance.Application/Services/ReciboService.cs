using AutoMapper;
using Finance.Application.Contratos;
using Finance.Application.Dtos.RecibosDtos;
using Finance.Domain;
using Finance.Domain.Enum;
using Finance.Persistence.Contratos;

namespace Finance.Application.Services;
public class ReciboService : IReciboService
{
    private readonly IReciboPersistence _reciboPersistence;
    private readonly ICobrancaPersistence _cobrancaPersistence;
    private readonly IMapper _mapper;

    public ReciboService(IReciboPersistence reciboPersistence, ICobrancaPersistence cobrancaPersistence, IMapper mapper)
    {
        _reciboPersistence = reciboPersistence;
        this._cobrancaPersistence = cobrancaPersistence;
        _mapper = mapper;
    }

    private bool IsCobrancaConcluida(Cobranca cobranca)
    {
        try
        {
            return cobranca.Status == CobrancaStatusEnum.Concluir;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    private bool IsCobrancaJaQuitada(Cobranca cobranca, Recibo[] recibosDb)
    {
        try
        {
            var valorTotalPago = recibosDb
                .Where(r => r.Status == ReciboStatusEnum.Ativo)
                .Sum(r => r.GetValorTotal());

            return cobranca.GetValorTotal() == valorTotalPago;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    private bool ValorReciboDividoCorretamenteCobrancaMensal(Cobranca cobranca, ReciboDto[] models)
    {
        try
        {
            return models.All(r => r.Valor + r.Acrescimo - r.Desconto == cobranca.GetValorMensal());
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    private bool ValorReciboIgualAoValorCobrancaAvista(Cobranca cobranca, ReciboDto[] models)
    {
        var recibosEnvioAtivos = models.Where(r => r.Status == ReciboStatusEnum.Ativo).ToArray();
        return
            recibosEnvioAtivos.Length == 1 &&
            recibosEnvioAtivos.Sum(r => r.Valor - r.Desconto + r.Acrescimo) == cobranca.Valor;
    }

    private bool ValorRecibosUltrapassado(Cobranca cobranca, Recibo[] recibosDb, ReciboDto[] models)
    {
        var valorTotalDb = recibosDb.Where(r => r.Status == ReciboStatusEnum.Ativo).Sum(r => r.GetValorTotal());
        var valorTotalModels = models.Where(c => c.Id == 0).Sum(m => m.Valor + m.Acrescimo - m.Desconto);

        var valorTotal = valorTotalDb + valorTotalModels;

        return valorTotal > cobranca.GetValorTotal();
    }


    private async Task AddRecibo(int userId, int cobrancaId, ReciboDto model)
    {
        try
        {

            var recibo = _mapper.Map<Recibo>(model);
            recibo.UserId = userId;
            recibo.CobrancaId = cobrancaId;

            _reciboPersistence.Add<Recibo>(recibo);

            await _reciboPersistence.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<ReciboDto[]> SaveRecibos(int userId, int cobrancaId, ReciboDto[] models)
    {
        try
        {
            var recibos = await _reciboPersistence.GetAllAsync(userId, cobrancaId);
            var cobranca = await _cobrancaPersistence.GetByIdAsync(userId, cobrancaId);

            if (cobranca.Status == CobrancaStatusEnum.Pago)
            {
                throw new Exception("A Cobrança já foi quitado");
            }

            if (!IsCobrancaConcluida(cobranca))
            {
                throw new Exception("A Cobrança precisa ser Concluída.");
            }

            if (cobranca.TipoCobranca == TipoCobrancaEnum.Avista && !ValorReciboIgualAoValorCobrancaAvista(cobranca, models))
            {
                throw new Exception("Cobrança com pagamento à vista, deve-se informar exatamente o valor informado na cobrança.");
            }

            if (ValorRecibosUltrapassado(cobranca, recibos, models))
            {
                throw new Exception("A soma dos valores dos recibos que irá ser salvo e os recibos já salvo no banco não pode ultrapassar do Valor da Cobrança.");
            }

            if (cobranca.TipoCobranca == TipoCobrancaEnum.Mensal && !ValorReciboDividoCorretamenteCobrancaMensal(cobranca, models))
            {
                throw new Exception("Todos os Recibos deve ter exatamente o valor da mensalidade a ser cobrada.");
            }

            foreach (var model in models)
            {
                if (model.Id == 0)
                {
                    await AddRecibo(userId, cobrancaId, model);
                }
            }

            var recibosReturn = await _reciboPersistence.GetAllAsync(userId, cobrancaId);

            if (cobranca.TipoCobranca != TipoCobrancaEnum.Indeterminado)
            {
                await ChangeStatusCobranca(cobranca, recibosReturn);
            }

            return _mapper.Map<ReciboDto[]>(recibosReturn);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<bool> CancelarRecibo(int userId, int cobrancaId, int reciboId)
    {
        try
        {

            var cobranca = await _cobrancaPersistence.GetByIdAsync(userId, cobrancaId);
            var recibos = await _reciboPersistence.GetAllAsync(userId, cobrancaId);

            var recibo = recibos.FirstOrDefault(r => r.Id == reciboId, null) ?? throw new Exception("Recibo não encontrado.");

            recibo.Status = ReciboStatusEnum.Cancelado;

            bool isConcluido = await ChangeStatusCobranca(cobranca, false);

            if (!isConcluido) throw new Exception("Erro ao tentar atualizar o status da Cobrança.");

            _reciboPersistence.Update<Recibo>(recibo);
            return await _reciboPersistence.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<bool> ChangeStatusCobranca(Cobranca cobranca, Recibo[] recibos)
    {
        try
        {
            bool IsPago = recibos.Where(r => r.Status == ReciboStatusEnum.Ativo).Sum(r => r.GetValorTotal()) == cobranca.Valor;

            return await ChangeStatusCobranca(cobranca, IsPago);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<bool> ChangeStatusCobranca(Cobranca cobranca, bool isPago)
    {
        if (cobranca.Status == CobrancaStatusEnum.Cancelado)
            throw new Exception($"Operação não permitdo, status da cobrança se encontra {cobranca.Status}.");

        if (isPago)
        {
            cobranca.Status = CobrancaStatusEnum.Pago;
        }
        else
        {
            cobranca.Status = CobrancaStatusEnum.Concluir;
        }

        _cobrancaPersistence.Update<Cobranca>(cobranca);

        return await _cobrancaPersistence.SaveChangesAsync();
    }

    public async Task<ReciboResponseDto[]> GetAllAsync(int userId, int cobrancaId)
    {
        try
        {
            var recibos = await _reciboPersistence.GetAllAsync(userId, cobrancaId);

            return _mapper.Map<ReciboResponseDto[]>(recibos);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<ReciboResponseDto> GetByIdAsync(int userId, int cobrancaId, int reciboId)
    {
        try
        {
            var recibo = await _reciboPersistence.GetByIdAsync(userId, cobrancaId, reciboId);

            return _mapper.Map<ReciboResponseDto>(recibo);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}