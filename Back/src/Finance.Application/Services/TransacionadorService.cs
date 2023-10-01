using AutoMapper;
using Finance.Application.Contratos;
using Finance.Application.Dtos.TransacionadoresDtos;
using Finance.Application.Helpers;
using Finance.Domain;
using Finance.Persistence.Contratos;
using Microsoft.EntityFrameworkCore;

namespace Finance.Application.Services;
public class TransacionadorService : ITransacionadorService
{
    private readonly ITransacionadorPersistence _transacionadorPersistence;
    private readonly IMapper _mapper;

    public TransacionadorService(ITransacionadorPersistence transacionadorPersistence, IMapper mapper)
    {
        _transacionadorPersistence = transacionadorPersistence;
        _mapper = mapper;
    }

    public async Task<TransacionadorResponseDto> AddAsync(int userId, TransacionadorRequestDto model)
    {
        try
        {
            var transacionador = _mapper.Map<Transacionador>(model);

            transacionador.Id = 0;
            transacionador.UserId = userId;
            transacionador.DataCadastro = DateTime.Now;
            transacionador.DataUltimaAlteracao = DateTime.Now;

            _transacionadorPersistence.Add<Transacionador>(transacionador);

            if (await _transacionadorPersistence.SaveChangesAsync())
            {
                var transacionadorRetorno = await _transacionadorPersistence.GetByIdAsync(userId, transacionador.Id);

                return _mapper.Map<TransacionadorResponseDto>(transacionadorRetorno);
            }

            return null;
        }
        catch (DbUpdateException ex)
        {
            throw new ExceptionServiceBadRequestError(ex.DbAddOrUpdateExceptionErroTreated());
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<TransacionadorResponseDto> UpdateAsync(int userId, int id, TransacionadorRequestDto model)
    {
        try
        {
            var transacionador = await _transacionadorPersistence.GetByIdAsync(userId, id);

            if (transacionador is null) return null;

            model.DataCadastro = transacionador.DataCadastro;

            _mapper.Map(model, transacionador);
            transacionador.Id = id;
            transacionador.UserId = userId;
            transacionador.DataUltimaAlteracao = DateTime.Now;

            _transacionadorPersistence.Update<Transacionador>(transacionador);

            if (await _transacionadorPersistence.SaveChangesAsync())
            {
                var transacionadorRetorno = await _transacionadorPersistence.GetByIdAsync(userId, id);

                return _mapper.Map<TransacionadorResponseDto>(transacionadorRetorno);
            }

            return null;

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<bool> DeleteAsync(int userId, int id)
    {
        try
        {
            var recebedorPagador = await _transacionadorPersistence.GetByIdAsync(userId, id);

            if (recebedorPagador is null) throw new Exception("Transacionador não foi encontrado para deletar.");

            _transacionadorPersistence.Delete<Transacionador>(recebedorPagador);

            return await _transacionadorPersistence.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<TransacionadorResponseDto[]> GetAllAsync(int userId)
    {
        var transacionadores = await _transacionadorPersistence.GetAllAsync(userId);

        return _mapper.Map<TransacionadorResponseDto[]>(transacionadores);
    }

    public async Task<TransacionadorResponseDto> GetByIdAsync(int userId, int id)
    {
        var transacionador = await _transacionadorPersistence.GetByIdAsync(userId, id);

        return _mapper.Map<TransacionadorResponseDto>(transacionador);
    }

}