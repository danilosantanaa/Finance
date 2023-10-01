using AutoMapper;
using Finance.Application.Contratos;
using Finance.Application.Dtos.CategoriaDtos;
using Finance.Application.Helpers;
using Finance.Domain;
using Finance.Persistence.Contratos;

namespace Finance.Application.Services;

public class CategoriaService : ICategoriaService
{
    private readonly ICategoriaPersistence _categoriaPersistence;
    private readonly IMapper _mapper;

    public CategoriaService(ICategoriaPersistence categoriaPersistence, IMapper mapper)
    {
        _categoriaPersistence = categoriaPersistence;
        _mapper = mapper;
    }
    public async Task<CategoriaDto> AddAsync(int userId, CategoriaDto model)
    {
        try
        {
            var categoria = _mapper.Map<Categoria>(model);
            categoria.Id = 0;
            categoria.UserId = userId;

            _categoriaPersistence.Add<Categoria>(categoria);

            if (await _categoriaPersistence.SaveChangesAsync())
            {
                var categoriaRetorno = await _categoriaPersistence.GetByIdAsync(userId, categoria.Id);

                return _mapper.Map<CategoriaDto>(categoriaRetorno);
            }

            return null;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<CategoriaDto> UpdateAsync(int userId, int id, CategoriaDto model)
    {
        try
        {
            var categoria = await _categoriaPersistence.GetByIdAsync(userId, id);

            if (categoria is null) return null;

            model.Id = categoria.Id;
            _mapper.Map(model, categoria);

            _categoriaPersistence.Update<Categoria>(categoria);

            if (await _categoriaPersistence.SaveChangesAsync())
            {
                var categoriaRetorno = await _categoriaPersistence.GetByIdAsync(userId, categoria.Id);

                return _mapper.Map<CategoriaDto>(categoriaRetorno);
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
            var categoria = await _categoriaPersistence.GetByIdAsync(userId, id);

            if (categoria is null) throw new ExceptionServiceBadRequestError("Categoria não encontrado.");

            _categoriaPersistence.Delete<Categoria>(categoria);

            return await _categoriaPersistence.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<CategoriaDto[]> GetAllAsync(int userId)
    {
        try
        {
            var categorias = await _categoriaPersistence.GetAllAsync(userId);

            return _mapper.Map<CategoriaDto[]>(categorias);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<CategoriaDto> GetByIdAsync(int userId, int id)
    {
        try
        {
            var categoria = await _categoriaPersistence.GetByIdAsync(userId, id);

            return _mapper.Map<CategoriaDto>(categoria);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
