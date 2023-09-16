using AutoMapper;
using Finance.Application.Dtos.BancoDtos;
using Finance.Application.Dtos.CategoriaDtos;
using Finance.Application.Dtos.CobrancaDtos;
using Finance.Application.Dtos.EnderecosDtos;
using Finance.Application.Dtos.IdentityDto;
using Finance.Application.Dtos.MovimentacaoDtos;
using Finance.Application.Dtos.RecibosDtos;
using Finance.Application.Dtos.TransacionadoresDtos;
using Finance.Domain;
using Finance.Domain.Identity;

namespace Finance.Application.Helpers
{
    public class FinanceProfile : Profile
    {
        public FinanceProfile()
        {
            #region DOMINIO
            CreateMap<Banco, BancoDto>().ReverseMap();
            CreateMap<Transacionador, TransacionadorRequestDto>().ReverseMap();
            CreateMap<Transacionador, TransacionadorResponseDto>().ReverseMap();
            CreateMap<Cidade, CidadeDto>().ReverseMap();
            CreateMap<Estado, EstadoDto>().ReverseMap();
            CreateMap<Endereco, EnderecoResponseDto>().ReverseMap();
            CreateMap<Endereco, EnderecoRequestDto>().ReverseMap();
            CreateMap<Categoria, CategoriaDto>().ReverseMap();
            CreateMap<Cobranca, CobrancaDto>().ReverseMap();
            CreateMap<Recibo, ReciboDto>().ReverseMap();
            CreateMap<Recibo, ReciboResponseDto>().ReverseMap();
            CreateMap<Movimentacao, MovimentacaoDto>().ReverseMap();
            #endregion

            #region IDENTITY
            CreateMap<User, UserResponseDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, UserLoginDto>().ReverseMap();
            CreateMap<User, UserUpdateDto>().ReverseMap();

            #endregion
        }
    }
}