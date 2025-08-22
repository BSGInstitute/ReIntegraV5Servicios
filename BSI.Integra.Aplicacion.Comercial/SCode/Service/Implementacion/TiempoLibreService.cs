using AutoMapper;
using BSI.Integra.Aplicacion.Comercial.Service.Interface;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Comercial.Service.Implementacion
{
    /// Service: TiempoLibreService
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 09/06/2022
    /// <summary>
    /// Gestión general de T_TiempoLibre
    /// </summary>
    public class TiempoLibreService : ITiempoLibreService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public TiempoLibreService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<TTiempoLibre, TiempoLibre>(MemberList.None).ReverseMap();
                    cfg.CreateMap<TiempoLibreDTO, ComboDTO>(MemberList.None);
                }
            );
            _mapper = new Mapper(config);
        }
    }
}
