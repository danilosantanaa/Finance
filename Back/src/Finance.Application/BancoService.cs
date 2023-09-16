using AutoMapper;
using Finance.Application.Contratos;
using Finance.Application.Dtos.BancoDtos;
using Finance.Application.Helpers;
using Finance.Domain;
using Finance.Persistence.Contratos;

namespace Finance.Application
{
    public class BancoService : IBancoService
    {
        private readonly IBancoPersistence _bancoPersistence;
        private readonly IMapper _mapper;

        public BancoService(IBancoPersistence bancoPersistence, IMapper mapper)
        {
            _bancoPersistence = bancoPersistence;
            _mapper = mapper;
        }
        public async Task<BancoDto> AddAsync(int userId, BancoDto model)
        {
            try
            {
                var banco = _mapper.Map<Banco>(model);
                banco.UserId = userId;
                banco.DataCadastro.SetDateNowDefault();
                banco.DataUltimaAlteracao.SetDateNowDefault();
                banco.Id = 0;

                _bancoPersistence.Add<Banco>(banco);

                if (await _bancoPersistence.SaveChangesAsync())
                {
                    var bancoRetorno = await _bancoPersistence.GetByIdAsync(userId, banco.Id);

                    return _mapper.Map<BancoDto>(bancoRetorno);
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<BancoDto> UpdateAsync(int userId, int bancoId, BancoDto model)
        {
            try
            {
                var banco = await _bancoPersistence.GetByIdAsync(userId, bancoId);
                if (banco is null) return null;

                model.Id = banco.Id;
                model.DataCadastro.SetDateNowDefault(banco.DataCadastro);

                _mapper.Map(model, banco);
                banco.UserId = userId;
                banco.DataUltimaAlteracao.SetDateNowDefault();

                _bancoPersistence.Update(banco);

                if (await _bancoPersistence.SaveChangesAsync())
                {
                    var bancoRetorno = await _bancoPersistence.GetByIdAsync(userId, banco.Id);

                    return _mapper.Map<BancoDto>(bancoRetorno);
                }

                return null;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteAsync(int userId, int bancoId)
        {
            try
            {
                var banco = await _bancoPersistence.GetByIdAsync(userId, bancoId);
                if (banco is null) throw new ExceptionServiceBadRequestError("Banco não encontrado para deletar.");

                _bancoPersistence.Delete<Banco>(banco);

                return await _bancoPersistence.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<BancoDto[]> GetAllAsync(int userId)
        {
            try
            {
                var bancos = await _bancoPersistence.GetAllAsync(userId);

                return _mapper.Map<BancoDto[]>(bancos);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<BancoDto> GetByIdAsync(int userId, int id)
        {
            try
            {
                var banco = await _bancoPersistence.GetByIdAsync(userId, id);

                return _mapper.Map<BancoDto>(banco);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}