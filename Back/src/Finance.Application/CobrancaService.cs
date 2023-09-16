using AutoMapper;
using Finance.Application.Contratos;
using Finance.Application.Dtos.CobrancaDtos;
using Finance.Application.Helpers;
using Finance.Domain;
using Finance.Domain.Enum;
using Finance.Persistence.Contratos;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Finance.Application
{
    public class CobrancaService : ICobrancaService
    {
        private readonly ICobrancaPersistence _cobrancaPersistence;
        private readonly IMapper _mapper;

        public CobrancaService(ICobrancaPersistence cobrancaPersistence, IMapper mapper)
        {
            _cobrancaPersistence = cobrancaPersistence;
            _mapper = mapper;
        }

        public void ValidarCobranca(Cobranca cobranca)
        {
            try
            {

                if (cobranca.TipoCobranca == TipoCobrancaEnum.Mensal && cobranca.QuantidadeParcelas == 0)
                    throw new ExceptionServiceBadRequestError("Para cobrança do tipo Mensal, deve-se informar a quantidade.");

                if (cobranca.TipoCobranca == TipoCobrancaEnum.Mensal && cobranca.Valor == 0)
                    throw new ExceptionServiceBadRequestError("Para cobrança do tipo Mensal, o valor não pode ser zerado.");
            }
            catch (ExceptionServiceBadRequestError ex)
            {
                throw new ExceptionServiceBadRequestError(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<CobrancaDto> AddAsync(int userId, CobrancaDto model)
        {
            try
            {

                var cobranca = _mapper.Map<Cobranca>(model);
                cobranca.UserId = userId;
                cobranca.DataEmissao = DateTime.Now;
                cobranca.Recibos = null;
                cobranca.User = null;
                cobranca.Status = CobrancaStatusEnum.Lancado;
                cobranca.Id = 0;

                ValidarCobranca(cobranca);

                _cobrancaPersistence.Add<Cobranca>(cobranca);

                if (await _cobrancaPersistence.SaveChangesAsync())
                {
                    var cobrancaRetorno = await _cobrancaPersistence.GetByIdAsync(userId, cobranca.Id);

                    return _mapper.Map<CobrancaDto>(cobrancaRetorno);
                }

                return null;
            }
            catch (ExceptionServiceBadRequestError ex)
            {
                throw new ExceptionServiceBadRequestError(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<CobrancaDto> UpdateAsync(int userId, int id, CobrancaDto model)
        {
            try
            {
                var cobranca = await _cobrancaPersistence.GetByIdAsync(userId, id);
                if (cobranca is null) return null;
                if (cobranca.Status != CobrancaStatusEnum.Lancado) throw new Exception("Não é permitido editar a cobrança.");

                cobranca.Id = cobranca.Id;
                _mapper.Map(model, cobranca);

                cobranca.UserId = userId;
                cobranca.Recibos = null;

                ValidarCobranca(cobranca);

                _cobrancaPersistence.Update<Cobranca>(cobranca);
                if (await _cobrancaPersistence.SaveChangesAsync())
                {
                    var cobrancaRetorno = await _cobrancaPersistence.GetByIdAsync(userId, id);

                    return _mapper.Map<CobrancaDto>(cobrancaRetorno);
                }

                return null;
            }
            catch (ExceptionServiceBadRequestError ex)
            {
                throw new ExceptionServiceBadRequestError(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<CobrancaDto[]> GetAllAsync(int userId)
        {
            try
            {
                var cobrancas = await _cobrancaPersistence.GetAllAsync(userId);

                return _mapper.Map<CobrancaDto[]>(cobrancas);
            }
            catch (ExceptionServiceBadRequestError ex)
            {
                throw new ExceptionServiceBadRequestError(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<CobrancaDto> GetByIdAsync(int userId, int id)
        {
            try
            {
                var cobranca = await _cobrancaPersistence.GetByIdAsync(userId, id, true);

                return _mapper.Map<CobrancaDto>(cobranca);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> UpdateStatusAsync(int id, int userId, string status)
        {
            try
            {
                var cobranca = await _cobrancaPersistence.GetByIdAsync(userId, id, true);

                var statusChange = (CobrancaStatusEnum)Enum.Parse(typeof(CobrancaStatusEnum), status, true);
                var somarRecibos = cobranca.Recibos.Sum(r => r.GetValorTotal());

                var notCanChangeStatus =
                    statusChange == CobrancaStatusEnum.Pago && somarRecibos != cobranca.Valor ||
                    statusChange == CobrancaStatusEnum.Pago && statusChange != CobrancaStatusEnum.Cancelado ||
                    cobranca.Status == CobrancaStatusEnum.Cancelado;

                if (cobranca.TipoCobranca != TipoCobrancaEnum.Indeterminado && notCanChangeStatus)
                {
                    throw new ExceptionServiceBadRequestError("A operação não é permitido.");
                }

                cobranca.Recibos.ToList().ForEach(r => r.Banco = null);
                if (statusChange == CobrancaStatusEnum.Cancelado)
                {
                    foreach (var recibo in cobranca.Recibos)
                    {
                        recibo.Status = ReciboStatusEnum.Cancelado;
                    }
                }

                cobranca.Status = statusChange;
                _cobrancaPersistence.Update<Cobranca>(cobranca);

                return await _cobrancaPersistence.SaveChangesAsync();
            }
            catch (ExceptionServiceBadRequestError ex)
            {
                throw new ExceptionServiceBadRequestError(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}